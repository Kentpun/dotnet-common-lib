using Microsoft.EntityFrameworkCore;
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

        public int SaveChanges() => DbContext.SaveChanges();

        public Task<int> SaveChangesAsync() => DbContext.SaveChangesAsync();

        public void Dispose() => DbContext?.Dispose();
    }
}