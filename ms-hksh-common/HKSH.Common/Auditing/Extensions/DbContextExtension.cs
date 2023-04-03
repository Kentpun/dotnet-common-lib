using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace HKSH.Common.Auditing.Extensions
{
    /// <summary>
    /// Represents a plugin for Microsoft.EntityFrameworkCore to support automatically recording data changes history.
    /// </summary>
    public static class DbContextExtensions
    {
        /// <summary>
        /// Ensures the automatic history.
        /// </summary>
        /// <param name="context">The context.</param>
        public static void ApplyAuditingHistory(this DbContext context)
        {
            EntityEntry[] entityEntries = context.ChangeTracker.Entries().Where(a => a.State == EntityState.Modified || a.State == EntityState.Deleted || a.State == EntityState.Added).ToArray();
            foreach (EntityEntry item in entityEntries)
            {
                AuditHistory logItem = item.AuditHistory(() => new AuditHistory());
                if (logItem != null)
                {
                    context.Set<AuditHistory>().Add(logItem);
                }
            }
        }

        /// <summary>
        /// Configurations the database auditing.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="enableAuditing">The enable auditing.</param>
        public static void ConfigDbAuditing(this DbContext context, bool? enableAuditing)
        {
            if (enableAuditing.GetValueOrDefault())
            {
                context.Database.ExecuteSqlRaw(RawSql.AddAuditingHistoryTable);
            }
        }
    }
}