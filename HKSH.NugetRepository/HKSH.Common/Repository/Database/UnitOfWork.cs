using HKSH.Common.Auditing;
using HKSH.Common.Auditing.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System.Data;

namespace HKSH.Common.Repository.Database
{
    internal class UnitOfWork<TDbContext> : IUnitOfWork where TDbContext : DbContext
    {
        private readonly IRepositoryCurrentContext _currentContext;
        private readonly IServiceProvider _serviceProvider;

        public DbContext DbContext { get; }

        public UnitOfWork(TDbContext dbContext,
            IRepositoryCurrentContext currentContext,
            IServiceProvider serviceProvider)
        {
            DbContext = dbContext;
            _currentContext = currentContext;
            _serviceProvider = serviceProvider;
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

        public IRepository<T> GetStore<T>() where T : class => new Repository<T>(DbContext, _currentContext, _serviceProvider);

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