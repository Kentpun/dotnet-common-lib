using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace HKSH.Common.Repository.Database
{
    /// <summary>
    /// IRepository
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="IDisposable" />
    public interface IRepository<T> : IDisposable where T : class
    {
        /// <summary>
        /// Adds the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        void Add(T entity);

        /// <summary>
        /// Adds the specified entity and save and return entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        T AddSaveChange(T entity);

        /// <summary>
        /// Adds the range.
        /// </summary>
        /// <param name="entities">The entities.</param>
        void AddRange(IEnumerable<T> entities);

        /// <summary>
        /// Adds the range save change.
        /// </summary>
        /// <param name="entities">The entities.</param>
        /// <returns></returns>
        IEnumerable<T> AddRangeSaveChange(IEnumerable<T> entities);

        /// <summary>
        /// Attaches the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        EntityEntry<T> Attach(T entity);

        /// <summary>
        /// Modifies the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        void Modify(T entity);

        /// <summary>
        /// Modifies the no track.
        /// </summary>
        /// <param name="entity">The entity.</param>
        void ModifyNoTrack(T entity);

        /// <summary>
        /// Modifies the range.
        /// </summary>
        /// <param name="entities">The entities.</param>
        void ModifyRange(IEnumerable<T> entities);

        /// <summary>
        /// Deletes the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        void Delete(T entity);

        /// <summary>
        /// Batches the delete.
        /// </summary>
        /// <param name="entities">The entities.</param>
        void BatchDelete(IEnumerable<T> entities);

        /// <summary>
        /// Deletes the physical.
        /// </summary>
        /// <param name="entity">The entity.</param>
        void DeletePhysical(T entity);

        /// <summary>
        /// Batches the delete physical.
        /// </summary>
        /// <param name="entities">The entities.</param>
        void BatchDeletePhysical(IEnumerable<T> entities);

        /// <summary>
        /// Saves the changes.
        /// </summary>
        /// <returns></returns>
        int SaveChanges();

        /// <summary>
        /// Saves the changes.
        /// </summary>
        /// <param name="businessType">Type of the business.</param>
        /// <returns></returns>
        int SaveChanges(string businessType);

        /// <summary>
        /// Saves the changes asynchronous.
        /// </summary>
        /// <returns></returns>
        Task<int> SaveChangesAsync();

        /// <summary>
        /// Saves the changes asynchronous.
        /// </summary>
        /// <param name="businessType">Type of the business.</param>
        /// <returns></returns>
        Task<int> SaveChangesAsync(string businessType);

        /// <summary>
        /// Gets the entities.
        /// </summary>
        /// <value>
        /// The entities.
        /// </value>
        IQueryable<T> Entities { get; }

        /// <summary>
        /// Gets the no filters entities.
        /// </summary>
        /// <value>
        /// The no filters entities.
        /// </value>
        IQueryable<T> NoFiltersEntities { get; }

        /// <summary>
        /// Gets the track entities.
        /// </summary>
        /// <value>
        /// The track entities.
        /// </value>
        IQueryable<T> TrackEntities { get; }

        /// <summary>
        /// Gets the no filters track entities.
        /// </summary>
        /// <value>
        /// The no filters track entities.
        /// </value>
        IQueryable<T> NoFiltersTrackEntities { get; }

        /// <summary>
        /// Froms the SQL raw.
        /// </summary>
        /// <param name="sql">The SQL.</param>
        /// <param name="params">The parameters.</param>
        /// <returns></returns>
        IQueryable<T> FromSqlRaw(string sql, params object[] @params);

        /// <summary>
        /// Froms the SQL raw track.
        /// </summary>
        /// <param name="sql">The SQL.</param>
        /// <param name="params">The parameters.</param>
        /// <returns></returns>
        IQueryable<T> FromSqlRawTrack(string sql, params object[] @params);

        /// <summary>
        /// Gets the next sequence number.
        /// </summary>
        /// <param name="dependentSymbol">The dependent symbol.</param>
        /// <param name="startingNumber">The starting number.</param>
        /// <param name="paddingCount">The padding count.</param>
        /// <returns></returns>
        string GetNextSequenceNumber(string dependentSymbol, decimal startingNumber, int paddingCount);
    }
}