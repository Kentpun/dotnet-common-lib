using DotNetCore.CAP;
using HKSH.Common.AuditLogs;
using HKSH.Common.AuditLogs.Models;
using HKSH.Common.Base;
using HKSH.Common.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Data;
using System.Text;

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
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        /// <summary>
        /// Rollbacks this instance.
        /// </summary>
        public void Rollback()
        {
            DbContext.Database.CurrentTransaction?.Rollback();
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
                        sqlDataBuilder.AppendLine(@$"('{rows[i].TableName}',{long.Parse(rows[i].RowId ?? "0")},'{rows[i].Action}','{rows[i].Row}','{rows[i].Version}','{rows[i].UpdateBy}',GETDATE());");
                    }
                    else
                    {
                        sqlDataBuilder.AppendLine(@$"('{rows[i].TableName}',{long.Parse(rows[i].RowId ?? "0")},'{rows[i].Action}','{rows[i].Row}','{rows[i].Version}','{rows[i].UpdateBy}',GETDATE()),");
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