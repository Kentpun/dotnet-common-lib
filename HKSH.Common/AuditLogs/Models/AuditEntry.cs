using HKSH.Common.Extensions;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Newtonsoft.Json;

namespace HKSH.Common.AuditLogs.Models
{
    /// <summary>
    /// AuditEntry
    /// </summary>
    public class AuditEntry
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AuditEntry"/> class.
        /// </summary>
        /// <param name="entry">The entry.</param>
        public AuditEntry(EntityEntry entry)
        {
            Entry = entry;
        }

        /// <summary>
        /// Gets the entry.
        /// </summary>
        /// <value>
        /// The entry.
        /// </value>
        public EntityEntry Entry { get; }

        /// <summary>
        /// Gets or sets the name of the table.
        /// </summary>
        /// <value>
        /// The name of the table.
        /// </value>
        public string TableName { get; set; } = null!;

        /// <summary>
        /// Gets or sets the type of the business.
        /// </summary>
        /// <value>
        /// The type of the business.
        /// </value>
        public string BusinessType { get; set; } = null!;

        /// <summary>
        /// Gets or sets the section.
        /// </summary>
        /// <value>
        /// The section.
        /// </value>
        public string? Section { get; set; }

        /// <summary>
        /// Gets or sets the module.
        /// </summary>
        /// <value>
        /// The module.
        /// </value>
        public string? Module { get; set; }

        /// <summary>
        /// Gets or sets the update by.
        /// </summary>
        /// <value>
        /// The update by.
        /// </value>
        public string? UpdateBy { get; set; }

        /// <summary>
        /// Gets or sets the action.
        /// </summary>
        /// <value>
        /// The action.
        /// </value>
        public string Action { get; set; } = null!;

        /// <summary>
        /// Gets the key values.
        /// </summary>
        /// <value>
        /// The key values.
        /// </value>
        public Dictionary<string, object> KeyValues { get; } = new Dictionary<string, object>();

        /// <summary>
        /// Gets the old values.
        /// </summary>
        /// <value>
        /// The old values.
        /// </value>
        public Dictionary<string, object> OldValues { get; } = new Dictionary<string, object>();

        /// <summary>
        /// Creates new values.
        /// </summary>
        /// <value>
        /// The new values.
        /// </value>
        public Dictionary<string, object> NewValues { get; } = new Dictionary<string, object>();

        /// <summary>
        /// Gets the temporary properties.
        /// </summary>
        /// <value>
        /// The temporary properties.
        /// </value>
        public List<PropertyEntry> TemporaryProperties { get; } = new List<PropertyEntry>();

        /// <summary>
        /// Gets a value indicating whether this instance has temporary properties.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance has temporary properties; otherwise, <c>false</c>.
        /// </value>
        public bool HasTemporaryProperties => TemporaryProperties.Any();

        /// <summary>
        /// Converts to audit.
        /// </summary>
        /// <returns></returns>
        public RowAuditLogDocument ToAudit()
        {
            var audit = new RowAuditLogDocument
            {
                Action = Action,
                TableName = TableName,
                RowId = Entry.PrimaryKey(),
                Module = Module,
                BusinessType = BusinessType,
                Section = Section,
                Version = new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds().ToString(),
                UpdateBy = UpdateBy,
                Row = JsonConvert.SerializeObject(Entry.Entity, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore })
            };

            return audit;
        }
    }
}