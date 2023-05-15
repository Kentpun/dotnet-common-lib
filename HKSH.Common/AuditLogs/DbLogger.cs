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
public static class DbLogger
{
    /// <summary>
    /// Applies the audit log.
    /// </summary>
    /// <param name="context">The context.</param>
    /// <param name="businessType">Type of the business.</param>
    /// <returns></returns>
    public static List<RowAuditLog> ApplyAuditLog(this DbContext context, string? businessType = "")
    {
        var auditLogs = new List<RowAuditLog>();
        EntityEntry[] entityEntries = context.ChangeTracker.Entries().Where(a => a.State == EntityState.Modified || a.State == EntityState.Deleted || a.State == EntityState.Added).ToArray();
        foreach (EntityEntry item in entityEntries)
        {
            Console.WriteLine("进入循环");

            //有标记的才会记录审计日志
            if (item.Entity is not IAuditLog)
            {
                continue;
            }

            Console.WriteLine("当前模型可以记录日志");

            //构造审计日志
            RowAuditLog? auditLog = ConstructAuditLog(item, businessType);
            if (auditLog != null)
            {
                auditLogs.Add(auditLog);
            }
        }

        return auditLogs;
    }

    /// <summary>
    /// Constructs the audit log.
    /// </summary>
    /// <param name="entityEntry">The entity entry.</param>
    /// <returns></returns>
    private static RowAuditLog? ConstructAuditLog(EntityEntry entityEntry, string? businessType)
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