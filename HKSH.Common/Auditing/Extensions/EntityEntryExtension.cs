using HKSH.Common.Auditing.Attributes;
using HKSH.Common.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;

namespace HKSH.Common.Auditing.Extensions
{
    public static class EntityEntryExtension
    {
        public static string PrimaryKey(this EntityEntry entry)
        {
            var key = entry.Metadata.FindPrimaryKey();

            var values = new List<object>();
            foreach (var property in key?.Properties ?? new List<Property>())
            {
                var value = entry.Property(property.Name).CurrentValue;
                if (value != null)
                {
                    values.Add(value);
                }
            }

            return string.Join(",", values);
        }

        public static AuditHistory AuditHistory(this EntityEntry entry, Func<AuditHistory> createHistoryFactory)
        {
            if (IsEntityExcluded(entry))
            {
                return null;
            }

            var properties = GetPropertiesWithoutExcluded(entry);
            if (!(properties.Any(p => p.IsModified) || entry.State == EntityState.Deleted || entry.State == EntityState.Added))
            {
                return null;
            }

            var history = createHistoryFactory();
            history.TableName = entry.Metadata.GetTableName();
            history.EntityName = entry.Metadata.ShortName();
            history.CreatedAt = DateTime.UtcNow;
            var entityTracker = entry.Entity as IEntityTracker;
            history.CreatedBy = entityTracker?.CreatedBy;
            switch (entry.State)
            {
                case EntityState.Added:
                    WriteHistoryAddedState(history, properties);
                    break;

                case EntityState.Modified:
                    history.CreatedBy = entityTracker?.ModifiedBy;
                    WriteHistoryModifiedState(history, entry, properties);
                    break;

                case EntityState.Deleted:
                    var delTracker = entry.Entity as IEntityDelTracker;
                    history.CreatedBy = delTracker?.DeletedBy;
                    WriteHistoryDeletedState(history, entry, properties);
                    break;

                case EntityState.Detached:
                case EntityState.Unchanged:
                default:
                    throw new NotSupportedException("AuditHistory only support Deleted and Modified entity.");
            }

            return history;
        }

        private static bool IsEntityExcluded(EntityEntry entry) =>
            entry.Metadata.ClrType.GetCustomAttributes(typeof(ExcludeAuditingAttribute), true).Any();

        private static IEnumerable<PropertyEntry> GetPropertiesWithoutExcluded(EntityEntry entry)
        {
            // Get the mapped properties for the entity type.
            // (include shadow properties, not include navigations & references)
            var excludedProperties = entry.Metadata.ClrType.GetProperties()
                    .Where(p => p.GetCustomAttributes(typeof(ExcludeAuditingAttribute), true).Any())
                    .Select(p => p.Name);

            var properties = entry.Properties.Where(f => !excludedProperties.Contains(f.Metadata.Name));
            return properties;
        }

        private static void WriteHistoryAddedState(AuditHistory history, IEnumerable<PropertyEntry> properties)
        {
            dynamic json = new System.Dynamic.ExpandoObject();

            foreach (var prop in properties)
            {
                if (prop.Metadata.IsKey() || prop.Metadata.IsForeignKey())
                {
                    continue;
                }
                ((IDictionary<string, object>)json)[prop.Metadata.Name] = prop.CurrentValue;
            }

            // REVIEW: what's the best way to set the RowId?
            history.RowId = "0";
            history.Kind = EntityState.Added;

            history.Changes = JsonConvert.SerializeObject(json, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
        }

        private static void WriteHistoryModifiedState(AuditHistory history, EntityEntry entry, IEnumerable<PropertyEntry> properties)
        {
            dynamic json = new System.Dynamic.ExpandoObject();
            dynamic bef = new System.Dynamic.ExpandoObject();
            dynamic aft = new System.Dynamic.ExpandoObject();

            PropertyValues databaseValues;
            foreach (var prop in properties)
            {
                if (prop.IsModified)
                {
                    if (prop.OriginalValue != null)
                    {
                        if (!prop.OriginalValue.Equals(prop.CurrentValue))
                        {
                            ((IDictionary<string, object>)bef)[prop.Metadata.Name] = prop.OriginalValue;
                        }
                        else
                        {
                            databaseValues = entry.GetDatabaseValues();
                            var originalValue = databaseValues?.GetValue<object>(prop.Metadata.Name);
                            ((IDictionary<string, object>)bef)[prop.Metadata.Name] = originalValue;
                        }
                    }
                    else
                    {
                        ((IDictionary<string, object?>)bef)[prop.Metadata.Name] = default;
                    }

                    ((IDictionary<string, object>)aft)[prop.Metadata.Name] = prop.CurrentValue;
                }
            }

            ((IDictionary<string, object>)json)["before"] = bef;
            ((IDictionary<string, object>)json)["after"] = aft;

            history.RowId = entry.PrimaryKey();
            history.Kind = EntityState.Modified;
            history.Changes = JsonConvert.SerializeObject(json, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
        }

        private static void WriteHistoryDeletedState(AuditHistory history, EntityEntry entry, IEnumerable<PropertyEntry> properties)
        {
            dynamic json = new System.Dynamic.ExpandoObject();

            foreach (var prop in properties)
            {
                ((IDictionary<string, object>)json)[prop.Metadata.Name] = prop.OriginalValue;
            }
            history.RowId = entry.PrimaryKey();
            history.Kind = EntityState.Deleted;
            history.Changes = JsonConvert.SerializeObject(json, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
        }
    }
}