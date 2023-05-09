using HKSH.Common.Base;
using HKSH.Common.Extensions;
using HKSH.Common.Resources;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace HKSH.Common.Repository.Database
{
    /// <summary>
    /// Repository
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="MicroService.Repository.IRepository{T}" />
    internal class Repository<T> : IRepository<T> where T : class
    {
        private readonly DbContext _dbContext;
        private string? _currentUserId;
        private readonly IRepositoryCurrentContext _currentContext;
        private readonly IServiceProvider _serviceProvider;

        /// <summary>
        /// The database set
        /// </summary>
        private readonly DbSet<T> _dbSet;

        /// <summary>
        /// Initializes a new instance of the <see cref="Repository{T}"/> class.
        /// </summary>
        /// <param name="dbContext">The database context.</param>
        /// <param name="currentContext">The current user id.</param>
        public Repository(DbContext dbContext,
            IRepositoryCurrentContext currentContext,
            IServiceProvider serviceProvider)
        {
            _dbContext = dbContext;
            _currentContext = currentContext;
            _dbSet = _dbContext.Set<T>();
            _serviceProvider = serviceProvider;
        }

        public string CurrentUserId
        {
            get
            {
                if (string.IsNullOrEmpty(_currentUserId))
                {
                    _currentUserId = _currentContext.CurrentUser?.Account ?? ""; // todo
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
                if (string.IsNullOrEmpty(tracker.CreatedBy))
                {
                    tracker.CreatedBy = CurrentUserId;
                }
            }
            _dbSet.Add(entity);
        }

        /// <summary>
        /// Adds the specified entity and save and return entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public T AddSaveChange(T entity)
        {
            var tracker = entity as IEntityTracker;
            if (tracker != null)
            {
                tracker.CreatedAt = DateTime.UtcNow;
                if (string.IsNullOrEmpty(tracker.CreatedBy))
                {
                    tracker.CreatedBy = CurrentUserId;
                }
            }
            _dbSet.Add(entity);
            _dbContext.SaveChanges();
            return entity;
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
        /// Adds the range.
        /// </summary>
        /// <param name="entities">The entities.</param>
        public IEnumerable<T> AddRangeSaveChange(IEnumerable<T> entities)
        {
            var newItemList = new List<T>();
            foreach (T item in entities)
            {
                var newItem = AddSaveChange(item);
                newItemList.Add(newItem);
            }
            return newItemList;
        }

        public EntityEntry<T> Attach(T entity)
        {
            return _dbSet.Attach(entity);
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
                if (existedEntity?.Id == currentEntity?.Id)
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

        public virtual string GetNextSequenceNumber(string dependentSymbol, decimal startingNumber, int paddingCount)
        {
            var query = _dbContext.Database.SqlQuery<NextNumber>(RawSql.GetNextSequenceNumber,
                new SqlParameter("@Category", typeof(T).Name),
                new SqlParameter("@DependentSymbol", $"{dependentSymbol}{DateTime.Now.Year}"),
                new SqlParameter("@StartingNumber", startingNumber),
                new SqlParameter("@PaddingCount", paddingCount));

            var lastNumber = query.FirstOrDefault().NextSymbol;

            #region Temp log to watch project number

            var logger = _serviceProvider.GetService<ILogger<Repository<T>>>();
            logger.LogInformation("GetNextSequenceNumber-NextSymbol:{0}", lastNumber);
            logger.LogInformation("GetNextSequenceNumber-Database:{0}", _dbContext.Database.GetConnectionString());

            #endregion Temp log to watch project number

            return lastNumber;
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            if (_dbContext != null)
            {
                _dbContext.Dispose();
            }
        }
    }
}