using DotNetCore.CAP;
using HKSH.Common.AuditLogs;
using HKSH.Common.AuditLogs.Models;
using HKSH.Common.Base;
using HKSH.Common.Caching.Redis;
using HKSH.Common.Constants;
using HKSH.Common.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Data;

namespace HKSH.Common.Repository.Database
{
    /// <summary>
    /// UnitOfWork
    /// </summary>
    /// <typeparam name="TDbContext">The type of the database context.</typeparam>
    /// <seealso cref="IUnitOfWork" />
    internal class UnitOfWork<TDbContext> : IUnitOfWork where TDbContext : DbContext
    {
        /// <summary>
        /// The current context
        /// </summary>
        private readonly IRepositoryCurrentContext _currentContext;

        /// <summary>
        /// The service provider
        /// </summary>
        private readonly IServiceProvider _serviceProvider;

        /// <summary>
        /// Gets the database context.
        /// </summary>
        /// <value>
        /// The database context.
        /// </value>
        public DbContext DbContext { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="UnitOfWork{TDbContext}"/> class.
        /// </summary>
        /// <param name="dbContext">The database context.</param>
        /// <param name="currentContext">The current context.</param>
        /// <param name="serviceProvider">The service provider.</param>
        public UnitOfWork(TDbContext dbContext,
            IRepositoryCurrentContext currentContext,
            IServiceProvider serviceProvider)
        {
            DbContext = dbContext;
            _currentContext = currentContext;
            _serviceProvider = serviceProvider;
        }

        /// <summary>
        /// Begins the transaction.
        /// </summary>
        /// <param name="isolationLevel">The isolation level.</param>
        public void BeginTransaction(IsolationLevel isolationLevel = IsolationLevel.Unspecified)
        {
            if (DbContext.Database.CurrentTransaction == null)
            {
                DbContext.Database.BeginTransaction(isolationLevel);
            }
        }

        /// <summary>
        /// Commits this instance.
        /// </summary>
        public void Commit()
        {
            var transaction = DbContext.Database.CurrentTransaction;
            if (transaction != null)
            {
                try
                {
                    transaction.Commit();

                    var redisRepository = _serviceProvider.GetService<IRedisRepository>();

                    var fields = redisRepository?.HashFields(CommonAuditLogConstants.TransactionRedisKey);
                    var currentTransitionFields = fields?.Where(s => s.Contains($"{transaction.TransactionId}")).ToList();

                    var rows = redisRepository?.HashScan<List<RowAuditLogDocument>>(CommonAuditLogConstants.TransactionRedisKey, $"{transaction.TransactionId}-*").SelectMany(row => row).ToList();

                    if (rows != null && rows.Any())
                    {
                        WriteAuditLogIntoDB(rows);

                        PublishAuditLogIntoEs(rows);

                        if (currentTransitionFields != null && currentTransitionFields.Any())
                        {
                            redisRepository?.HashDelete(CommonAuditLogConstants.TransactionRedisKey, currentTransitionFields);
                        }
                    }
                }
                catch
                {
                    Rollback();

                    throw;
                }
            }
        }

        /// <summary>
        /// Rollbacks this instance.
        /// </summary>
        public void Rollback()
        {
            var transaction = DbContext.Database.CurrentTransaction;

            if (transaction != null)
            {
                transaction.Rollback();

                var redisRepository = _serviceProvider.GetService<IRedisRepository>();

                var fields = redisRepository?.HashFields(CommonAuditLogConstants.TransactionRedisKey);
                var currentTransitionFields = fields?.Where(s => s.Contains($"{transaction.TransactionId}")).ToList();

                if (currentTransitionFields != null && currentTransitionFields.Any())
                {
                    redisRepository?.HashDelete(CommonAuditLogConstants.TransactionRedisKey, currentTransitionFields);
                }
            }
        }

        /// <summary>
        /// Gets the store.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public IRepository<T> GetStore<T>() where T : class => new Repository<T>(DbContext, _currentContext, _serviceProvider);

        /// <summary>
        /// do save
        /// </summary>
        /// <returns></returns>
        public int SaveChanges() => DbContext.SaveChanges();

        /// <summary>
        /// do save async
        /// </summary>
        /// <returns></returns>
        public Task<int> SaveChangesAsync() => DbContext.SaveChangesAsync();

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose() => DbContext?.Dispose();

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
                var result = DbContext.SaveChangesAsync();
                result.Wait();
                OnAfterSaveChanges(auditEntries).Wait();
                rows = auditEntries.Select(s => s.ToAudit()).ToList();
                if (result.Result > 0 && rows.Any())
                {
                    if (DbContext.Database.CurrentTransaction == null)
                    {
                        WriteAuditLogIntoDB(rows);

                        PublishAuditLogIntoEs(rows);
                    }
                    else
                    {
                        var redisRepository = _serviceProvider.GetService<IRedisRepository>();
                        redisRepository?.HashSet(CommonAuditLogConstants.TransactionRedisKey, $"{DbContext.Database.CurrentTransaction.TransactionId}-{Guid.NewGuid()}", rows);
                    }
                }
                return result.Result;
            }
            else
            {
                return DbContext.SaveChanges();
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
                var result = DbContext.SaveChangesAsync();
                result.Wait();
                OnAfterSaveChanges(auditEntries).Wait();
                rows = auditEntries.Select(s => s.ToAudit()).ToList();
                if (result.Result > 0 && rows.Any())
                {
                    if (DbContext.Database.CurrentTransaction == null)
                    {
                        WriteAuditLogIntoDB(rows);

                        PublishAuditLogIntoEs(rows);
                    }
                    else
                    {
                        var redisRepository = _serviceProvider.GetService<IRedisRepository>();
                        redisRepository?.HashSet(CommonAuditLogConstants.TransactionRedisKey, $"{DbContext.Database.CurrentTransaction.TransactionId}-{Guid.NewGuid()}", rows);
                    }
                }
                return result;
            }
            else
            {
                return DbContext.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Called when [before save changes].
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        private List<AuditEntry> OnBeforeSaveChanges(AuditLogParams request)
        {
            DbContext.ChangeTracker.DetectChanges();
            var auditEntries = new List<AuditEntry>();
            foreach (var entry in DbContext.ChangeTracker.Entries())
            {
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
        public void WriteAuditLogIntoDB(List<RowAuditLogDocument> rows)
        {
            var configuration = _serviceProvider.GetService<IConfiguration>();

            var connectionString = configuration?.GetConnectionString(AuditHistory.ChangeLogConnectionString) ?? string.Empty;

            var auditHistory = new AuditHistory(rows);

            if (!string.IsNullOrEmpty(AuditHistory.CreateAuditLogTableSql))
            {
                new DBHelperSqlServer(connectionString).ExecuteSql(AuditHistory.CreateAuditLogTableSql);
            }

            if (!string.IsNullOrEmpty(auditHistory.InsertAuditLogSql))
            {
                new DBHelperSqlServer(connectionString).ExecuteSql(auditHistory.InsertAuditLogSql);
            }
        }

        /// <summary>
        /// Publishes the audit log into es.
        /// </summary>
        /// <param name="rows">The rows.</param>
        private void PublishAuditLogIntoEs(List<RowAuditLogDocument> rows)
        {
            var message = new LogMqRequest
            {
                Uuid = Guid.NewGuid(),
                Action = CommonAuditLogConstants.AuditLogAction,
                Log = JsonConvert.SerializeObject(rows)
            };
            var publisher = _serviceProvider.GetService<ICapPublisher>();
            publisher?.Publish(CapTopic.AuditLogs, message);
        }

        #endregion AuditLog
    }
}