using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HKSH.Common.Base
{
    /// <summary>
    /// BaseTypeConfiguration<T>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="IEntityTypeConfiguration<T>" />
    public class BaseTypeConfiguration<T> : IEntityTypeConfiguration<T> where T : class, IEntityIdentify<long>
    {
        /// <summary>
        /// Configures the entity of type <typeparamref name="TEntity" />.
        /// </summary>
        /// <param name="builder">The builder to be used to configure the entity type.</param>
        public virtual void Configure(EntityTypeBuilder<T> builder)
        {
            builder.Property(p => p.Id).HasIdentityOptions();
        }
    }
}