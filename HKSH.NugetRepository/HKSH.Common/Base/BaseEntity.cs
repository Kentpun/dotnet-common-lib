using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HKSH.Common.Base
{
    /// <summary>
    /// BaseEntity
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="IEntityIdentify&lt;T&gt;" />
    public abstract class BaseEntity<T> : IEntityIdentify<T>
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        [Column("id")]
        [Key]
        public T Id { get; set; }
    }

    public abstract class BaseEntity : BaseEntity<long>
    {
    }
}