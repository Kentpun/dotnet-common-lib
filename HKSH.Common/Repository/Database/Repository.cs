using DotNetCore.CAP;
using HKSH.Common.AuditLogs;
using HKSH.Common.AuditLogs.Models;
using HKSH.Common.Base;
using HKSH.Common.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Data;
using System.Text;

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
        /// Initializes a new instance of the <see cref="Repository{T}"/> class.
        /// </summary>
        /// <param name="dbContext">The database context.</param>
        /// <param name="currentContext">The current context.</param>
        /// <param name="serviceProvider">The service provider.</param>
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
                    _currentUserId = _currentContext.CurrentUser?.Id.ToString() ?? "";
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
                tracker.CreatedAt = DateTime.Now;
                tracker.ModifiedAt = DateTime.Now;
                if (string.IsNullOrEmpty(tracker.CreatedBy))
                {
                    tracker.CreatedBy = CurrentUserId;
                    tracker.ModifiedBy = CurrentUserId;
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
                tracker.CreatedAt = DateTime.Now;
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
                tracker.ModifiedAt = DateTime.Now;
                tracker.ModifiedBy = CurrentUserId;
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
                tracker.DeletedAt = DateTime.Now;
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
            _dbContext?.Dispose();
        }

        #region Overloaded

        /// <summary>
        /// Adds the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="userId">The user identifier.</param>
        public void Add(T entity, string userId)
        {
            var tracker = entity as IEntityTracker;
            if (tracker != null)
            {
                tracker.CreatedAt = DateTime.Now;
                tracker.ModifiedAt = DateTime.Now;
                if (string.IsNullOrEmpty(tracker.CreatedBy))
                {
                    tracker.CreatedBy = userId;
                    tracker.ModifiedBy = userId;
                }
            }
            _dbSet.Add(entity);
        }

        /// <summary>
        /// Adds the range.
        /// </summary>
        /// <param name="entities">The entities.</param>
        /// <param name="userId">The user identifier.</param>
        public void AddRange(IEnumerable<T> entities, string userId)
        {
            foreach (T item in entities)
            {
                Add(item, userId);
            }
        }

        /// <summary>
        /// Modifies the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="userId">The user identifier.</param>
        public void Modify(T entity, string userId)
        {
            var tracker = entity as IEntityTracker;
            if (tracker != null)
            {
                tracker.ModifiedAt = DateTime.Now;
                tracker.ModifiedBy = userId;
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

        public void ModifyRange(IEnumerable<T> entities, string userId)
        {
            foreach (T item in entities)
            {
                Modify(item, userId);
            }
        }

        /// <summary>
        /// Deletes the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="userId">The user identifier.</param>
        public void Delete(T entity, string userId)
        {
            var tracker = entity as IEntityDelTracker;
            if (tracker != null)
            {
                tracker.DeletedAt = DateTime.Now;
                tracker.IsDeleted = true;
                if (string.IsNullOrEmpty(tracker.DeletedBy))
                {
                    tracker.DeletedBy = userId;
                }
            }
            _dbSet.Update(entity);
        }

        /// <summary>
        /// Batches the delete.
        /// </summary>
        /// <param name="entities">The entities.</param>
        /// <param name="userId">The user identifier.</param>
        public void BatchDelete(IEnumerable<T> entities, string userId)
        {
            foreach (T item in entities)
            {
                Delete(item, userId);
            }
        }

        #endregion Overloaded

        #region AuditLog

        /// <summary>
        /// Saves the changes.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        public int SaveChanges(AuditLogParams request)
        {
            var dbLogSettings = _serviceProvider.GetService<IOptions<EnableAuditLogOptions>>();
            if (dbLogSettings?.Value?.IsEnabled == true)
            {
                var rows = new List<RowAuditLogDocument>();
                var auditEntries = OnBeforeSaveChanges(request);
                var result = _dbContext.SaveChangesAsync();
                result.Wait();
                OnAfterSaveChanges(auditEntries).Wait();
                rows = auditEntries.Select(s => s.ToAudit()).ToList();
                if (result.Result > 0 && rows.Any())
                {
                    WriteAuditLogIntoDB(rows);

                    var message = new LogMqRequest
                    {
                        Uuid = Guid.NewGuid(),
                        Action = "change",
                        Log = JsonConvert.SerializeObject(rows)
                    };
                    var publisher = _serviceProvider.GetService<ICapPublisher>();
                    publisher?.Publish(CapTopic.AuditLogs, message);
                }
                return result.Result;
            }
            else
            {
                return _dbContext.SaveChanges();
            }
        }

        /// <summary>
        /// Saves the changes asynchronous.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        public Task<int> SaveChangesAsync(AuditLogParams request)
        {
            var dbLogSettings = _serviceProvider.GetService<IOptions<EnableAuditLogOptions>>();
            if (dbLogSettings?.Value?.IsEnabled == true)
            {
                var rows = new List<RowAuditLogDocument>();
                var auditEntries = OnBeforeSaveChanges(request);
                var result = _dbContext.SaveChangesAsync();
                result.Wait();
                OnAfterSaveChanges(auditEntries).Wait();
                rows = auditEntries.Select(s => s.ToAudit()).ToList();
                if (result.Result > 0 && rows.Any())
                {
                    WriteAuditLogIntoDB(rows);

                    var message = new LogMqRequest
                    {
                        Uuid = Guid.NewGuid(),
                        Action = "change",
                        Log = JsonConvert.SerializeObject(rows)
                    };
                    var publisher = _serviceProvider.GetService<ICapPublisher>();
                    publisher?.Publish(CapTopic.AuditLogs, message);
                }
                return result;
            }
            else
            {
                return _dbContext.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Called when [before save changes].
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        private List<AuditEntry> OnBeforeSaveChanges(AuditLogParams request)
        {
            _dbContext.ChangeTracker.DetectChanges();
            var auditEntries = new List<AuditEntry>();
            foreach (var entry in _dbContext.ChangeTracker.Entries())
            {
                //繼承了IAuditLog的數據庫對象才可以記錄日誌
                if (entry.Entity is not IAuditLog || entry.State == EntityState.Detached || entry.State == EntityState.Unchanged)
                    continue;

                var entityTracker = entry.Entity as IEntityTracker;
                var entityDelTracker = entry.Entity as IEntityDelTracker;

                var auditEntry = new AuditEntry(entry)
                {
                    TableName = entry.Metadata.GetTableName() ?? string.Empty,
                    Module = request.Module,
                    BusinessType = request.BusinessType,
                    Section = request.Section,
                    Action = entityDelTracker?.IsDeleted ?? false ? EntityState.Deleted.ToString() : entry.State.ToString()
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
                        auditEntry.KeyValues[propertyName] = property.CurrentValue ?? string.Empty;
                        continue;
                    }

                    switch (entry.State)
                    {
                        case EntityState.Added:
                            auditEntry.UpdateBy = entityTracker?.CreatedBy;
                            auditEntry.NewValues[propertyName] = property.CurrentValue ?? string.Empty;
                            break;

                        case EntityState.Deleted:
                            auditEntry.UpdateBy = entityDelTracker?.DeletedBy;
                            auditEntry.OldValues[propertyName] = property.OriginalValue ?? string.Empty;
                            break;

                        case EntityState.Modified:
                            auditEntry.UpdateBy = entityTracker?.ModifiedBy;
                            if (property.IsModified)
                            {
                                auditEntry.OldValues[propertyName] = property.OriginalValue ?? string.Empty;
                                auditEntry.NewValues[propertyName] = property.CurrentValue ?? string.Empty;
                            }
                            break;
                    }
                }
            }

            // keep a list of entries where the value of some properties are unknown at this step
            return auditEntries.ToList();
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
                        auditEntry.KeyValues[prop.Metadata.Name] = prop.CurrentValue ?? string.Empty;
                    }
                    else
                    {
                        auditEntry.NewValues[prop.Metadata.Name] = prop.CurrentValue ?? string.Empty;
                    }
                }
            }

            return SaveChangesAsync();
        }

        /// <summary>
        /// Writes the audit log into database.
        /// </summary>
        /// <param name="rows">The rows.</param>
        private void WriteAuditLogIntoDB(List<RowAuditLogDocument> rows)
        {
            StringBuilder tableSqlBuilder = new();

            tableSqlBuilder.AppendLine(@$"BEGIN TRAN IF NOT EXISTS (");
            tableSqlBuilder.AppendLine(@$"  SELECT");
            tableSqlBuilder.AppendLine(@$"    TOP 1 *");
            tableSqlBuilder.AppendLine(@$"  FROM");
            tableSqlBuilder.AppendLine(@$"    sysObjects");
            tableSqlBuilder.AppendLine(@$"  WHERE");
            tableSqlBuilder.AppendLine(@$"    Id = OBJECT_ID('com_audit_{DateTime.Now:yyyyMM}')");
            tableSqlBuilder.AppendLine(@$"    and xtype = 'U'");
            tableSqlBuilder.AppendLine(@$") BEGIN CREATE TABLE com_audit_{DateTime.Now:yyyyMM} (");
            tableSqlBuilder.AppendLine(@$"  [id] [bigint] IDENTITY(1, 1) NOT NULL,");
            tableSqlBuilder.AppendLine(@$"  [table_name] [nvarchar] (200) NULL,");
            tableSqlBuilder.AppendLine(@$"  [row_id][bigint] NULL,");
            tableSqlBuilder.AppendLine(@$"  [action][nvarchar] (100) NULL,");
            tableSqlBuilder.AppendLine(@$"  [row][nvarchar] (max)NULL,");
            tableSqlBuilder.AppendLine(@$"  [version][nvarchar] (max)NULL,");
            tableSqlBuilder.AppendLine(@$"  [created_by][nvarchar] (100) NULL,");
            tableSqlBuilder.AppendLine(@$"  [created_at] [datetime2](7) NULL CONSTRAINT [PK_com_audit_202206] PRIMARY KEY CLUSTERED ([id] ASC) WITH (");
            tableSqlBuilder.AppendLine(@$"    STATISTICS_NORECOMPUTE = OFF,");
            tableSqlBuilder.AppendLine(@$"    IGNORE_DUP_KEY = OFF,");
            tableSqlBuilder.AppendLine(@$"    OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF");
            tableSqlBuilder.AppendLine(@$"  ) ON [PRIMARY]");
            tableSqlBuilder.AppendLine(@$") ON [PRIMARY]");
            tableSqlBuilder.AppendLine(@$"END COMMIT TRAN");

            var configuration = _serviceProvider.GetService<IConfiguration>();

            var connectionString = configuration?.GetConnectionString("SqlServer") ?? string.Empty;

            string tableSql = tableSqlBuilder.ToString();
            if (!string.IsNullOrEmpty(tableSql))
            {
                new DBHelperSqlServer(connectionString).ExecuteSql(tableSql);
            }

            if (rows != null && rows.Any())
            {
                StringBuilder sqlDataBuilder = new();
                sqlDataBuilder.AppendLine(@$"INSERT INTO com_audit_{DateTime.Now:yyyyMM} ([table_name],[row_id],[action],[row],[version],[created_by],[created_at])");
                sqlDataBuilder.AppendLine(@$"VALUES");
                for (int i = 0; i < rows.Count; i++)
                {
                    if (i == rows.Count - 1)
                    {
                        sqlDataBuilder.AppendLine(@$"(N'{rows[i].TableName}',{long.Parse(rows[i].RowId ?? "0")},N'{rows[i].Action}',N'{rows[i].Row}',N'{rows[i].Version}',N'{rows[i].UpdateBy}',GETDATE());");
                    }
                    else
                    {
                        sqlDataBuilder.AppendLine(@$"(N'{rows[i].TableName}',{long.Parse(rows[i].RowId ?? "0")},N'{rows[i].Action}',N'{rows[i].Row}',N'{rows[i].Version}',N'{rows[i].UpdateBy}',GETDATE()),");
                    }
                }

                string dataSql = sqlDataBuilder.ToString();
                if (!string.IsNullOrEmpty(dataSql))
                {
                    new DBHelperSqlServer(connectionString).ExecuteSql(dataSql);
                }
            }
        }

        #endregion AuditLog
    }
}