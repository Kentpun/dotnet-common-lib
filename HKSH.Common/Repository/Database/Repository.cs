using DotNetCore.CAP;
using HKSH.Common.AuditLogs;
using HKSH.Common.AuditLogs.Models;
using HKSH.Common.Base;
using HKSH.Common.Constants;
using HKSH.Common.Extensions;
using HKSH.Common.Resources;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace HKSH.Common.Repository.Database
{
    /// <summary>
    /// Repository
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="IRepository&lt;T&gt;" />
    internal class Repository<T> : IRepository<T> where T : class
    {
        /// <summary>
        /// The database context
        /// </summary>
        private readonly DbContext _dbContext;

        /// <summary>
        /// The current user identifier
        /// </summary>
        private string? _currentUserId;

        /// <summary>
        /// The current context
        /// </summary>
        private readonly IRepositoryCurrentContext _currentContext;

        /// <summary>
        /// The service provider
        /// </summary>
        private readonly IServiceProvider _serviceProvider;

        /// <summary>
        /// The database set
        /// </summary>
        private readonly DbSet<T> _dbSet;

        /// <summary>
        /// Repository
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="currentContext"></param>
        /// <param name="serviceProvider"></param>
        public Repository(DbContext dbContext,
            IRepositoryCurrentContext currentContext,
            IServiceProvider serviceProvider)
        {
            _dbContext = dbContext;
            _currentContext = currentContext;
            _dbSet = _dbContext.Set<T>();
            _serviceProvider = serviceProvider;
        }

        /// <summary>
        /// Gets the current user identifier.
        /// </summary>
        /// <value>
        /// The current user identifier.
        /// </value>
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
        /// <returns></returns>
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
        /// <returns></returns>
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

        /// <summary>
        /// Attaches the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
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
        /// Saves the changes.
        /// </summary>
        /// <param name="auditLogRequest">The audit log request.</param>
        /// <returns></returns>
        public int SaveChanges(AuditLogRequest? auditLogRequest)
        {
            var dbLogSettings = _serviceProvider.GetService<IOptions<EnableAuditLogOptions>>();
            if (dbLogSettings?.Value?.IsEnabled == true)
            {
                var logs = _dbContext.ApplyAuditLog(auditLogRequest);
                if (logs.Any())
                {
                    var publisher = _serviceProvider.GetService<ICapPublisher>();
                    publisher?.Publish(CapTopic.AuditLogs, logs);
                }
            }

            return _dbContext.SaveChanges();
        }

        /// <summary>
        /// Saves the changes asynchronous.
        /// </summary>
        /// <returns></returns>
        public Task<int> SaveChangesAsync() => _dbContext.SaveChangesAsync();

        /// <summary>
        /// Saves the changes asynchronous.
        /// </summary>
        /// <param name="auditLogRequest">The audit log request.</param>
        /// <returns></returns>
        public Task<int> SaveChangesAsync(AuditLogRequest? auditLogRequest)
        {
            var dbLogSettings = _serviceProvider.GetService<IOptions<EnableAuditLogOptions>>();
            if (dbLogSettings?.Value?.IsEnabled == true)
            {
                var rows = new List<RowAuditLog>();
                var auditEntries = OnBeforeSaveChanges(auditLogRequest?.BusinessCode);
                var result = _dbContext.SaveChangesAsync();
                result.Wait();
                OnAfterSaveChanges(auditEntries).Wait();
                rows = auditEntries.Select(s => s.ToAudit()).ToList();
                if (result.Result > 0 && rows.Any())
                {
                    var publisher = _serviceProvider.GetService<ICapPublisher>();
                    publisher?.Publish(CapTopic.AuditLogs, rows);
                }
                return result;
            }
            else
            {
                return _dbContext.SaveChangesAsync();
            }
        }

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
        /// Gets the next sequence number.
        /// </summary>
        /// <param name="dependentSymbol">The dependent symbol.</param>
        /// <param name="startingNumber">The starting number.</param>
        /// <param name="paddingCount">The padding count.</param>
        /// <returns></returns>
        public virtual string GetNextSequenceNumber(string dependentSymbol, decimal startingNumber, int paddingCount)
        {
            var query = _dbContext.Database.SqlQuery<NextNumber>(RawSql.GetNextSequenceNumber,
                new SqlParameter("@Category", typeof(T).Name),
                new SqlParameter("@DependentSymbol", $"{dependentSymbol}{DateTime.Now.Year}"),
                new SqlParameter("@StartingNumber", startingNumber),
                new SqlParameter("@PaddingCount", paddingCount));

            var lastNumber = query?.FirstOrDefault()?.NextSymbol;

            #region Temp log to watch project number

            var logger = _serviceProvider.GetService<ILogger<Repository<T>>>();
            logger?.LogInformation("GetNextSequenceNumber-NextSymbol:{0}", lastNumber);
            logger?.LogInformation("GetNextSequenceNumber-Database:{0}", _dbContext.Database.GetConnectionString());

            #endregion Temp log to watch project number

            return lastNumber;
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            _dbContext?.Dispose();
        }

        #region Extention

        /// <summary>
        /// Called when [before save changes].
        /// </summary>
        /// <returns></returns>
        private List<AuditEntry> OnBeforeSaveChanges(string? businessCode)
        {
            _dbContext.ChangeTracker.DetectChanges();
            var auditEntries = new List<AuditEntry>();
            foreach (var entry in _dbContext.ChangeTracker.Entries())
            {
                if (entry.Entity is not IAuditLog || entry.State == EntityState.Detached || entry.State == EntityState.Unchanged)
                    continue;

                var entityTracker = entry.Entity as IEntityTracker;
                var entityDelTracker = entry.Entity as IEntityDelTracker;

                var auditEntry = new AuditEntry(entry)
                {
                    TableName = entry.Metadata.GetTableName() ?? "",
                    BusinessCode = businessCode,
                    Action = entry.State.ToString()
                };
                auditEntries.Add(auditEntry);

                foreach (var property in entry.Properties)
                {
                    if (property.IsTemporary)
                    {
                        // value will be generated by the database, get the value after saving
                        auditEntry.TemporaryProperties.Add(property);
                        continue;
                    }

                    string propertyName = property.Metadata.Name;
                    if (property.Metadata.IsPrimaryKey())
                    {
                        auditEntry.KeyValues[propertyName] = property.CurrentValue ?? "";
                        continue;
                    }

                    switch (entry.State)
                    {
                        case EntityState.Added:
                            auditEntry.UpdateBy = entityTracker?.CreatedBy;
                            auditEntry.NewValues[propertyName] = property.CurrentValue ?? "";
                            break;

                        case EntityState.Deleted:
                            auditEntry.UpdateBy = entityDelTracker?.DeletedBy;
                            auditEntry.OldValues[propertyName] = property.OriginalValue ?? "";
                            break;

                        case EntityState.Modified:
                            auditEntry.UpdateBy = entityTracker?.CreatedBy;
                            if (property.IsModified)
                            {
                                auditEntry.OldValues[propertyName] = property.OriginalValue ?? "";
                                auditEntry.NewValues[propertyName] = property.CurrentValue ?? "";
                            }
                            break;
                    }
                }
            }

            // Save audit entities that have all the modifications
            foreach (var auditEntry in auditEntries.Where(_ => !_.HasTemporaryProperties))
            {
                //Audits.Add(auditEntry.ToAudit());
            }

            // keep a list of entries where the value of some properties are unknown at this step
            return auditEntries.Where(_ => _.HasTemporaryProperties).ToList();
        }

        /// <summary>
        /// Called when [after save changes].
        /// </summary>
        /// <param name="auditEntries">The audit entries.</param>
        /// <returns></returns>
        private Task OnAfterSaveChanges(List<AuditEntry> auditEntries)
        {
            if (auditEntries == null || auditEntries.Count == 0)
                return Task.CompletedTask;

            foreach (var auditEntry in auditEntries)
            {
                // Get the final value of the temporary properties
                foreach (var prop in auditEntry.TemporaryProperties)
                {
                    if (prop.Metadata.IsPrimaryKey())
                    {
                        auditEntry.KeyValues[prop.Metadata.Name] = prop.CurrentValue ?? "";
                    }
                    else
                    {
                        auditEntry.NewValues[prop.Metadata.Name] = prop.CurrentValue ?? "";
                    }
                }
            }

            return SaveChangesAsync();
        }

        #endregion Extention
    }
}