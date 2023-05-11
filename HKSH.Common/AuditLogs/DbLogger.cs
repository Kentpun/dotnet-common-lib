using DotNetCore.CAP;
using HKSH.Common.AuditLogs.Models;
using HKSH.Common.Base;
using HKSH.Common.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;

namespace HKSH.Common.AuditLogs;

/// <summary>
/// DbLogger
/// </summary>
public class DbLogger
{
    /// <summary>
    /// The database context
    /// </summary>
    private readonly DbContext _dbContext;

    /// <summary>
    /// The audit logs
    /// </summary>
    private readonly List<RowAuditLog> auditLogs = new();

    /// <summary>
    /// The business type
    /// </summary>
    private readonly string? businessType = string.Empty;

    /// <summary>
    /// The cap bus
    /// </summary>
    private readonly ICapPublisher? _capBus;

    /// <summary>
    /// Initializes a new instance of the <see cref="DbLogger"/> class.
    /// </summary>
    /// <param name="dbContext">The database context.</param>
    /// <param name="capBus">The cap bus.</param>
    public DbLogger(DbContext dbContext, ICapPublisher? capBus, string? businessType)
    {
        _dbContext = dbContext;
        _capBus = capBus;
        this.businessType = businessType;
    }

    /// <summary>
    /// Enables the database log.
    /// </summary>
    public void EnableDbLog()
    {
#pragma warning disable CS8622 // Nullability of reference types in type of parameter doesn't match the target delegate (possibly because of nullability attributes).
        _dbContext.SavingChanges += DbContext_SavingChanges;
#pragma warning restore CS8622 // Nullability of reference types in type of parameter doesn't match the target delegate (possibly because of nullability attributes).
#pragma warning disable CS8622 // Nullability of reference types in type of parameter doesn't match the target delegate (possibly because of nullability attributes).
        _dbContext.SavedChanges += DbContext_SavedChanges;
#pragma warning restore CS8622 // Nullability of reference types in type of parameter doesn't match the target delegate (possibly because of nullability attributes).
#pragma warning disable CS8622 // Nullability of reference types in type of parameter doesn't match the target delegate (possibly because of nullability attributes).
        _dbContext.SaveChangesFailed += DbContext_SaveChangesFailed;
#pragma warning restore CS8622 // Nullability of reference types in type of parameter doesn't match the target delegate (possibly because of nullability attributes).
    }

    /// <summary>
    /// Handles the SavingChanges event of the DbContext control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="SavingChangesEventArgs"/> instance containing the event data.</param>
    /// <returns></returns>
    private void DbContext_SavingChanges(object sender, SavingChangesEventArgs e)
    {
      
    }

    /// <summary>
    /// Handles the SavedChanges event of the DbContext control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="SavedChangesEventArgs"/> instance containing the event data.</param>
    /// <returns></returns>
    private void DbContext_SavedChanges(object sender, SavedChangesEventArgs e)
    {
        EntityEntry[] entityEntries = _dbContext.ChangeTracker.Entries().Where(a => a.State == EntityState.Modified || a.State == EntityState.Deleted || a.State == EntityState.Added).ToArray();
        foreach (EntityEntry item in entityEntries)
        {
            //有标记的才会记录审计日志
            if (item.Entity is not IAuditLog)
            {
                continue;
            }

            //构造审计日志
            RowAuditLog? auditLog = ConstructAuditLog(item);
            if (auditLog != null)
            {
                auditLogs.Add(auditLog);
            }
        }

        //Kafka推送消息队列
        if (auditLogs != null && auditLogs.Any())
        {
            _capBus?.Publish(CapTopic.AuditLogs, auditLogs);
        }
    }

    /// <summary>
    /// Handles the SaveChangesFailed event of the DbContext control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="SaveChangesFailedEventArgs"/> instance containing the event data.</param>
    /// <returns></returns>
    private void DbContext_SaveChangesFailed(object sender, SaveChangesFailedEventArgs e)
    {

    }

    /// <summary>
    /// Constructs the audit log.
    /// </summary>
    /// <param name="entityEntry">The entity entry.</param>
    /// <returns></returns>
    private RowAuditLog? ConstructAuditLog(EntityEntry entityEntry)
    {
        Type type = entityEntry.Entity.GetType();
        var entityTracker = entityEntry.Entity as IEntityTracker;
        IEnumerable<PropertyInfo> properties = type.GetProperties().Where(a => !a.CustomAttributes.Any(b => b.AttributeType == typeof(NotMappedAttribute)));
        var entityIdTracker = entityEntry.Entity as IEntityIdentify<long>;

        var serializeSettings = new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore };

        string? updateBy = string.Empty;
        switch (entityEntry.State)
        {
            case EntityState.Detached:
                break;
            case EntityState.Unchanged:
                break;
            case EntityState.Deleted:
                var entityDelTracker = entityEntry.Entity as IEntityDelTracker;
                updateBy = entityDelTracker?.DeletedBy;
                break;
            case EntityState.Modified:
                updateBy = entityTracker?.ModifiedBy;
                break;
            case EntityState.Added:
                updateBy = entityTracker?.CreatedBy;
                break;
            default:
                break;
        }

        var row = new RowAuditLog
        {
            TableName = type.Name,
            TableId = entityIdTracker?.Id ?? 0,
            Action = entityEntry.State.ToString(),
            UpdateBy = updateBy,
            BusinessCode = businessType ?? "",
            Version = new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds().ToString(),
            Row = JsonConvert.SerializeObject(entityEntry.Entity, serializeSettings)
        };

        Console.WriteLine("保存事件：" + JsonConvert.SerializeObject(row));

        return row;
    }
}