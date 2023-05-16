using DotNetCore.CAP;
using HKSH.Common.AuditLogs;
using HKSH.Common.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System.Data;
using System.Security.Policy;

namespace HKSH.Common.Repository.Database
{
    /// <summary>
    /// UnitOfWork
    /// </summary>
    /// <typeparam name="TDbContext">The type of the database context.</typeparam>
    /// <seealso cref="IUnitOfWork" />
    internal class UnitOfWork<TDbContext> : IUnitOfWork where TDbContext : DbContext
    {
        /// <summary>
        /// The current context
        /// </summary>
        private readonly IRepositoryCurrentContext _currentContext;

        /// <summary>
        /// The service provider
        /// </summary>
        private readonly IServiceProvider _serviceProvider;

        /// <summary>
        /// Gets the database context.
        /// </summary>
        /// <value>
        /// The database context.
        /// </value>
        public DbContext DbContext { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="UnitOfWork{TDbContext}"/> class.
        /// </summary>
        /// <param name="dbContext">The database context.</param>
        /// <param name="currentContext">The current context.</param>
        /// <param name="serviceProvider">The service provider.</param>
        public UnitOfWork(TDbContext dbContext,
            IRepositoryCurrentContext currentContext,
            IServiceProvider serviceProvider)
        {
            DbContext = dbContext;
            _currentContext = currentContext;
            _serviceProvider = serviceProvider;
        }

        /// <summary>
        /// Begins the transaction.
        /// </summary>
        /// <param name="isolationLevel">The isolation level.</param>
        public void BeginTransaction(IsolationLevel isolationLevel = IsolationLevel.Unspecified)
        {
            if (DbContext.Database.CurrentTransaction == null)
            {
                DbContext.Database.BeginTransaction(isolationLevel);
            }
        }

        /// <summary>
        /// Commits this instance.
        /// </summary>
        public void Commit()
        {
            var transaction = DbContext.Database.CurrentTransaction;
            if (transaction != null)
            {
                try
                {
                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        /// <summary>
        /// Rollbacks this instance.
        /// </summary>
        public void Rollback()
        {
            DbContext.Database.CurrentTransaction?.Rollback();
        }

        /// <summary>
        /// Gets the store.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public IRepository<T> GetStore<T>() where T : class => new Repository<T>(DbContext, _currentContext, _serviceProvider);

        /// <summary>
        /// do save
        /// </summary>
        /// <returns></returns>
        public int SaveChanges() => DbContext.SaveChanges();

        /// <summary>
        /// Saves the changes.
        /// </summary>
        /// <param name="businessType">Type of the business.</param>
        /// <returns></returns>
        public int SaveChanges(string businessType)
        {
            var dbLogSettings = _serviceProvider.GetService<IOptions<EnableAuditLogOptions>>();
            if (dbLogSettings?.Value?.IsEnabled == true)
            {
                Console.WriteLine("允许记录日志");
                var logs = DbContext.ApplyAuditLog(businessType);
                if (logs.Any())
                {
                    var publisher = _serviceProvider.GetService<ICapPublisher>();
                    Console.WriteLine("成功发送消息");
                    publisher?.Publish(CapTopic.AuditLogs, logs);
                }
            }

            return DbContext.SaveChanges();
        }

        /// <summary>
        /// do save async
        /// </summary>
        /// <returns></returns>
        public Task<int> SaveChangesAsync() => DbContext.SaveChangesAsync();

        /// <summary>
        /// Saves the changes asynchronous.
        /// </summary>
        /// <param name="businessType">Type of the business.</param>
        /// <returns></returns>
        public Task<int> SaveChangesAsync(string businessType)
        {
            var dbLogSettings = _serviceProvider.GetService<IOptions<EnableAuditLogOptions>>();
            if (dbLogSettings?.Value?.IsEnabled == true)
            {
                var logs = DbContext.ApplyAuditLog(businessType);
                if (logs.Any())
                {
                    var publisher = _serviceProvider.GetService<ICapPublisher>();
                    publisher?.Publish(CapTopic.AuditLogs, logs);
                }
            }

            return DbContext.SaveChangesAsync();
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose() => DbContext?.Dispose();
    }
}