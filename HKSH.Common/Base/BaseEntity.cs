using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HKSH.Common.Base
{
    /// <summary>
    /// BaseEntity
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class BaseEntity<T> : IEntityIdentify<T>
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
    }

    /// <summary>
    /// BaseEntity
    /// </summary>
    public abstract class BaseEntity : BaseEntity<long>
    {
    }
}