namespace HKSH.Common.AuditLogs.Models
{
    /// <summary>
    /// Field AuditLog Document
    /// </summary>
    public class FieldAuditLogDocument : BaseLogModel
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public string? Id { get; set; }

        /// <summary>
        /// Gets or sets the module.
        /// </summary>
        /// <value>
        /// The module.
        /// </value>
        public string? Module { get; set; }

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
        /// Gets or sets the field.
        /// </summary>
        /// <value>
        /// The field.
        /// </value>
        public string Field { get; set; } = null!;

        /// <summary>
        /// Gets or sets the update from.
        /// </summary>
        /// <value>
        /// The update from.
        /// </value>
        public string UpdateFrom { get; set; } = null!;

        /// <summary>
        /// Gets or sets the update to.
        /// </summary>
        /// <value>
        /// The update to.
        /// </value>
        public string UpdateTo { get; set; } = null!;

        /// <summary>
        /// Gets or sets the action.
        /// </summary>
        /// <value>
        /// The action.
        /// </value>
        public string Action { get; set; } = null!;
    }
}