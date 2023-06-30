using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HKSH.Common.AuditLogs.Models
{
    /// <summary>
    /// Audit History
    /// </summary>
    public class AuditHistory
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        [Key]
        [Column("id")]
        public long Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the table.
        /// </summary>
        /// <value>
        /// The name of the table.
        /// </value>
        [Column("table_name")]
        [MaxLength(200)]
        public string? TableName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the row identifier.
        /// </summary>
        /// <value>
        /// The row identifier.
        /// </value>
        [Column("row_id")]
        public long? RowId { get; set; }

        /// <summary>
        /// Gets or sets the action.
        /// </summary>
        /// <value>
        /// The action.
        /// </value>
        [Column("action")]
        [MaxLength(100)]
        public string? Action { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the row.
        /// </summary>
        /// <value>
        /// The row.
        /// </value>
        [Column("row")]
        public string? Row { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the version.
        /// </summary>
        /// <value>
        /// The version.
        /// </value>
        [Column("version")]
        public string? Version { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the created at.
        /// </summary>
        /// <value>
        /// The created at.
        /// </value>
        [Column("created_at")]
        public DateTime? CreatedAt { get; set; }

        /// <summary>
        /// Gets or sets the created by.
        /// </summary>
        /// <value>
        /// The created by.
        /// </value>
        [Column("created_by")]
        [MaxLength(100)]
        public string? CreatedBy { get; set; } = string.Empty;
    }
}