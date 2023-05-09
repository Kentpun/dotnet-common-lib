using HKSH.Common.Attributes;
using HKSH.Common.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HKSH.Common.ShareModel
{
    /// <summary>
    /// NLog
    /// </summary>
    [NoneUnifiedPrefix]
    public class LogNLog : BaseEntity
    {
        /// <summary>
        /// Gets or sets the logged.
        /// </summary>
        /// <value>
        /// The logged.
        /// </value>
        [Required]
        [Column("timestamp")]
        public DateTime Timestamp { get; set; }

        /// <summary>
        /// Gets or sets the level.
        /// </summary>
        /// <value>
        /// The level.
        /// </value>
        [MaxLength(64)]
        [Required]
        [Column("level")]
        public string? Level { get; set; }

        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        /// <value>
        /// The message.
        /// </value>
        [Required]
        [Column("message", TypeName = "nvarchar(max)")]
        public string? Message { get; set; }

        /// <summary>
        /// Gets or sets the logger.
        /// </summary>
        /// <value>
        /// The logger.
        /// </value>
        [MaxLength(256)]
        [Column("logger")]
        public string? Logger { get; set; }

        /// <summary>
        /// Gets or sets the callsite.
        /// </summary>
        /// <value>
        /// The callsite.
        /// </value>
        [Column("callsite", TypeName = "nvarchar(max)")]
        public string? Callsite { get; set; }

        /// <summary>
        /// Gets or sets the exception.
        /// </summary>
        /// <value>
        /// The exception.
        /// </value>
        [Column("exception", TypeName = "nvarchar(max)")]
        public string? Exception { get; set; }
    }
}