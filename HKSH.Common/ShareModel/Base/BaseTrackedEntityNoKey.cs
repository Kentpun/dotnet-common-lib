using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HKSH.Common.ShareModel.Base
{
    /// <summary>
    /// BaseTrackedEntity None Primary Key
    /// </summary>
    /// <seealso cref="IEntityTracker" />
    /// <seealso cref="IEntityDelTracker" />
    [NotMapped]
    public class BaseTrackedEntityNoKey : IEntityTracker, IEntityDelTracker
    {
        /// <summary>
        /// Gets or sets the created time.
        /// </summary>
        /// <value>
        /// The created time.
        /// </value>
        public DateTime CreatedTime { get; set; }

        /// <summary>
        /// Gets or sets the created by.
        /// </summary>
        /// <value>
        /// The created by.
        /// </value>
        [MaxLength(100)]
        public string CreatedBy { get; set; } = null!;

        /// <summary>
        /// Gets or sets the modified time.
        /// </summary>
        /// <value>
        /// The modified time.
        /// </value>
        public DateTime? ModifiedTime { get; set; }

        /// <summary>
        /// Gets or sets the modified by.
        /// </summary>
        /// <value>
        /// The modified by.
        /// </value>
        [MaxLength(100)]
        public string? ModifiedBy { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is deleted.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is deleted; otherwise, <c>false</c>.
        /// </value>
        public byte RecordStatus { get; set; }

        /// <summary>
        /// Gets or sets the deleted time.
        /// </summary>
        /// <value>
        /// The deleted time.
        /// </value>
        public DateTime? DeletedTime { get; set; }

        /// <summary>
        /// Gets or sets the deleted by.
        /// </summary>
        /// <value>
        /// The deleted by.
        /// </value>
        [MaxLength(100)]
        public string? DeletedBy { get; set; }

        /// <summary>
        /// System Trace ID
        /// </summary>
        /// <value>
        /// The trace identifier.
        /// </value>
        [MaxLength(100)]
        public string? TraceId { get; set; }
    }
}
