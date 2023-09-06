namespace HKSH.Common.AuditLogs.Models
{
    /// <summary>
    ///RowAuditLogDocument
    /// </summary>
    public class RowAuditLogDocument : BaseLogModel
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public string? Id { get; set; }

        /// <summary>
        /// Gets or sets the action.
        /// </summary>
        /// <value>
        /// The action.
        /// </value>
        public string Action { get; set; } = null!;

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
        /// Gets or sets the row identifier.
        /// </summary>
        /// <value>
        /// The row identifier.
        /// </value>
        public string? RowId { get; set; }

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
        /// Gets or sets the version.
        /// </summary>
        /// <value>
        /// The version.
        /// </value>
        public string Version { get; set; } = null!;

        /// <summary>
        /// Gets or sets the row.
        /// </summary>
        /// <value>
        /// The row.
        /// </value>
        public string Row { get; set; } = null!;
    }
}