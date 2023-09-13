using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HKSH.Common.ShareModel.Base
{
    /// <summary>
    /// BaseEntity<T>
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
        public T Id { get; set; } = default!;
    }

    /// <summary>
    /// BaseEntity
    /// </summary>
    public abstract class BaseEntity : BaseEntity<long>
    { }
}