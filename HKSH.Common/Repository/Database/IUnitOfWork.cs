using Microsoft.EntityFrameworkCore;
using System.Data;

namespace HKSH.Common.Repository.Database
{
    /// <summary>
    /// IUnitOfWork
    /// </summary>
    /// <seealso cref="IDisposable" />
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// Gets the database context.
        /// </summary>
        /// <value>
        /// The database context.
        /// </value>
        DbContext DbContext { get; }

        /// <summary>
        /// Begins the transaction.
        /// </summary>
        /// <param name="isolationLevel">The isolation level.</param>
        void BeginTransaction(IsolationLevel isolationLevel = IsolationLevel.Unspecified);

        /// <summary>
        /// Commits this instance.
        /// </summary>
        void Commit();

        /// <summary>
        /// Rollbacks this instance.
        /// </summary>
        void Rollback();

        /// <summary>
        /// do save async
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<int> SaveChangesAsync();

        /// <summary>
        /// Saves the changes asynchronous.
        /// </summary>
        /// <param name="businessType">Type of the business.</param>
        /// <param name="module">The module.</param>
        /// <param name="section">The section.</param>
        /// <returns></returns>
        Task<int> SaveChangesAsync(string businessType, string module, string? section = "");

        /// <summary>
        /// do save
        /// </summary>
        /// <returns></returns>
        int SaveChanges();

        /// <summary>
        /// Saves the changes.
        /// </summary>
        /// <param name="businessType">Type of the business.</param>
        /// <param name="module">The module.</param>
        /// <param name="section">The section.</param>
        /// <returns></returns>
        int SaveChanges(string businessType, string module, string? section = "");

        /// <summary>
        /// Gets the store.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        IRepository<T> GetStore<T>() where T : class;
    }
}