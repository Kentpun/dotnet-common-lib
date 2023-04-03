using Microsoft.EntityFrameworkCore;
using System.Data;

namespace HKSH.Common.Repository.Database
{
    public interface IUnitOfWork : IDisposable
    {
        DbContext DbContext { get; }

        void BeginTransaction(IsolationLevel isolationLevel = IsolationLevel.Unspecified);

        void Commit();

        void Rollback();

        /// <summary>
        /// do save async
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<int> SaveChangesAsync(CancellationToken token = default(CancellationToken));

        /// <summary>
        /// do save
        /// </summary>
        /// <returns></returns>
        int SaveChanges();

        IRepository<T> GetStore<T>() where T : class;
    }
}