using HKSH.Common.Auditing;
using HKSH.Common.Auditing.Extensions;
using HKSH.Common.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System.Data;

namespace HKSH.Common.Repository.Database
{
    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="TDbContext">The type of the database context.</typeparam>
    /// <seealso cref="IBasicUnitOfWork" />
    internal class BasicUnitOfWork : IBasicUnitOfWork
    {
        private readonly IRepositoryCurrentContext _currentContext;

        public DbContext DbContext { get; }

        private readonly IServiceProvider _serviceProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="BasicUnitOfWork{TDbContext}"/> class.
        /// </summary>
        /// <param name="dbContext">The database context.</param>
        /// <param name="currentContext">The current context.</param>
        public BasicUnitOfWork(IBasicDbContext dbContext,
            IServiceProvider serviceProvider,
            IRepositoryCurrentContext currentContext)
        {
            DbContext = dbContext as DbContext;
            _serviceProvider = serviceProvider;
            _currentContext = currentContext;
        }

        public void BeginTransaction(IsolationLevel isolationLevel = IsolationLevel.Unspecified)
        {
            if (DbContext.Database.CurrentTransaction == null)
            {
                DbContext.Database.BeginTransaction(isolationLevel);
            }
        }

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

        public void Rollback()
        {
            if (DbContext.Database.CurrentTransaction != null)
            {
                DbContext.Database.CurrentTransaction.Rollback();
            }
        }

        public IBasicRepository<T> GetStore<T>() where T : class
        {
            return new BasicRepository<T>(DbContext as IBasicDbContext, _currentContext, _serviceProvider);
        }

        public int SaveChanges()
        {
            var dbLogSettings = _serviceProvider.GetService<IOptions<AuditingOptions>>();
            if (dbLogSettings?.Value?.EnableAuditing == true)
            {
                DbContext.ApplyAuditingHistory();
            }
            return DbContext.SaveChanges();
        }

        public Task<int> SaveChangesAsync(CancellationToken token = default)
        {
            if (token == default)
            {
                token = new CancellationTokenSource(30 * 1000).Token;
            }
            var dbLogSettings = _serviceProvider.GetService<IOptions<AuditingOptions>>();
            if (dbLogSettings?.Value?.EnableAuditing == true)
            {
                DbContext.ApplyAuditingHistory();
            }

            return DbContext.SaveChangesAsync(token);
        }

        public void Dispose() => DbContext?.Dispose();
    }
}