using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HKSH.Common.Base
{
    /// <summary>
    /// Basic Entity
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="IEntityTracker" />
    /// <seealso cref="IEntityDelTracker" />
    [NotMapped]
    public class BaseTrackedEntity<T> : IEntityTracker, IEntityDelTracker, IEntityIdentify<T>
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        [Key]
        [Column("id")]
        public T Id { get; set; }

        /// <summary>
        /// Gets or sets the created at.
        /// </summary>
        /// <value>
        /// The created at.
        /// </value>
        [Column("created_at")]
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Gets or sets the created by.
        /// </summary>
        /// <value>
        /// The created by.
        /// </value>
        [Column("created_by")]
        [MaxLength(100)]
        public string CreatedBy { get; set; } = null!;

        /// <summary>
        /// Gets or sets the modified at.
        /// </summary>
        /// <value>
        /// The modified at.
        /// </value>
        [Column("modified_at")]
        public DateTime? ModifiedAt { get; set; }

        /// <summary>
        /// Gets or sets the modified by.
        /// </summary>
        /// <value>
        /// The modified by.
        /// </value>
        [Column("modified_by")]
        [MaxLength(100)]
        public string? ModifiedBy { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is deleted.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is deleted; otherwise, <c>false</c>.
        /// </value>
        [Column("is_deleted")]
        public bool IsDeleted { get; set; }

        /// <summary>
        /// Gets or sets the deleted at.
        /// </summary>
        /// <value>
        /// The deleted at.
        /// </value>
        [Column("deleted_at")]
        public DateTime? DeletedAt { get; set; }

        /// <summary>
        /// Gets or sets the deleted by.
        /// </summary>
        /// <value>
        /// The deleted by.
        /// </value>
        [Column("deleted_by")]
        [MaxLength(100)]
        public string? DeletedBy { get; set; }
    }

    /// <summary>
    /// Base TrackedEntity
    /// </summary>
    [NotMapped]
    public class BaseTrackedEntity : BaseTrackedEntity<long>
    { }
}