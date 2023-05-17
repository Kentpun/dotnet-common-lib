using HKSH.Common.Base;
using Microsoft.EntityFrameworkCore;

namespace HKSH.Common.Repository.Database
{
    /// <summary>
    /// Repository
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="MicroService.Repository.IRepository{T}" />
    internal class BasicRepository<T> : IBasicRepository<T> where T : class
    {
        private readonly DbContext _dbContext;
        private string? _currentUserId;
        private readonly IRepositoryCurrentContext _currentContext;

        /// <summary>
        /// The database set
        /// </summary>
        private readonly DbSet<T> _dbSet;

        /// <summary>
        /// Initializes a new instance of the <see cref="BasicRepository{T}"/> class.
        /// </summary>
        /// <param name="dbContext">The database context.</param>
        /// <param name="currentContext">The current context.</param>
        public BasicRepository(IBasicDbContext dbContext, IRepositoryCurrentContext currentContext)
        {
            _dbContext = dbContext as DbContext;
            _currentContext = currentContext;
            _dbSet = _dbContext.Set<T>();
        }

        public string? CurrentUserId
        {
            get
            {
                if (string.IsNullOrEmpty(_currentUserId))
                {
                    _currentUserId = _currentContext?.CurrentUser?.Id.ToString() ?? "";
                }
                return _currentUserId;
            }
        }

        /// <summary>
        /// Adds the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public void Add(T entity)
        {
            var tracker = entity as IEntityTracker;
            if (tracker != null)
            {
                tracker.CreatedAt = DateTime.UtcNow;
                tracker.ModifiedAt = DateTime.UtcNow;
                if (string.IsNullOrEmpty(tracker.CreatedBy))
                {
                    tracker.CreatedBy = CurrentUserId ?? string.Empty;
                    tracker.ModifiedBy = CurrentUserId;
                }
            }
            _dbSet.Add(entity);
        }

        /// <summary>
        /// Adds the range.
        /// </summary>
        /// <param name="entities">The entities.</param>
        public void AddRange(IEnumerable<T> entities)
        {
            foreach (T item in entities)
            {
                Add(item);
            }
        }

        /// <summary>
        /// Modifies the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public void Modify(T entity)
        {
            var tracker = entity as IEntityTracker;
            if (tracker != null)
            {
                tracker.ModifiedAt = DateTime.UtcNow;
                if (string.IsNullOrEmpty(tracker.ModifiedBy))
                {
                    tracker.ModifiedBy = CurrentUserId;
                }
            }
            //tracked already
            foreach (var item in _dbSet.Local)
            {
                var existedEntity = item as IEntityIdentify<long>;
                var currentEntity = entity as IEntityIdentify<long>;
                if (existedEntity?.Id == currentEntity.Id)
                {
                    _dbSet.Local.Remove(item);
                    break;
                }
            }
            _dbSet.Update(entity);
        }

        /// <summary>
        /// Modifies the no track.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public void ModifyNoTrack(T entity)
        {
            _dbSet.Update(entity);
        }

        /// <summary>
        /// Modifies the range.
        /// </summary>
        /// <param name="entities">The entities.</param>
        public void ModifyRange(IEnumerable<T> entities)
        {
            foreach (T item in entities)
            {
                Modify(item);
            }
        }

        /// <summary>
        /// Deletes the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public void Delete(T entity)
        {
            var tracker = entity as IEntityDelTracker;
            if (tracker != null)
            {
                tracker.DeletedAt = DateTime.UtcNow;
                tracker.IsDeleted = true;
                if (string.IsNullOrEmpty(tracker.DeletedBy))
                {
                    tracker.DeletedBy = CurrentUserId;
                }
            }
            _dbSet.Update(entity);
        }

        /// <summary>
        /// Batches the delete.
        /// </summary>
        /// <param name="entities">The entities.</param>
        public void BatchDelete(IEnumerable<T> entities)
        {
            foreach (T item in entities)
            {
                Delete(item);
            }
        }

        /// <summary>
        /// Deletes the physical.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public void DeletePhysical(T entity) => _dbSet.Remove(entity);

        /// <summary>
        /// Batches the delete physical.
        /// </summary>
        /// <param name="entities">The entities.</param>
        public void BatchDeletePhysical(IEnumerable<T> entities) => _dbSet.RemoveRange(entities);

        /// <summary>
        /// Saves the changes.
        /// </summary>
        /// <returns></returns>
        public int SaveChanges() => _dbContext.SaveChanges();

        /// <summary>
        /// Saves the changes asynchronous.
        /// </summary>
        /// <returns></returns>
        public Task<int> SaveChangesAsync() => _dbContext.SaveChangesAsync();

        /// <summary>
        /// Gets the entities.
        /// </summary>
        /// <value>
        /// The entities.
        /// </value>
        public IQueryable<T> Entities => _dbSet.AsNoTracking();

        /// <summary>
        /// Gets the no filters entities.
        /// </summary>
        /// <value>
        /// The no filters entities.
        /// </value>
        public IQueryable<T> NoFiltersEntities => _dbSet.IgnoreQueryFilters().AsNoTracking();

        /// <summary>
        /// Gets the track entities.
        /// </summary>
        /// <value>
        /// The track entities.
        /// </value>
        public IQueryable<T> TrackEntities => _dbSet;

        /// <summary>
        /// Gets the no filters track entities.
        /// </summary>
        /// <value>
        /// The no filters track entities.
        /// </value>
        public IQueryable<T> NoFiltersTrackEntities => _dbSet.IgnoreQueryFilters();

        /// <summary>
        /// Froms the SQL raw.
        /// </summary>
        /// <param name="sql">The SQL.</param>
        /// <param name="params">The parameters.</param>
        /// <returns></returns>
        public IQueryable<T> FromSqlRaw(string sql, params object[] @params) => _dbSet.FromSqlRaw(sql, @params).AsNoTracking();

        /// <summary>
        /// Froms the SQL raw track.
        /// </summary>
        /// <param name="sql">The SQL.</param>
        /// <param name="params">The parameters.</param>
        /// <returns></returns>
        public IQueryable<T> FromSqlRawTrack(string sql, params object[] @params) => _dbSet.FromSqlRaw(sql, @params);

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            if (_dbContext != null)
            {
                _dbContext.Database.CloseConnection();
                _dbContext.Dispose();
            }
        }
    }
}