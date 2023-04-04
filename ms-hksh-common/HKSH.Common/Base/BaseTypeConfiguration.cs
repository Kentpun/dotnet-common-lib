using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HKSH.Common.Base
{
    public class BaseTypeConfiguration<T> : IEntityTypeConfiguration<T> where T : class, IEntityIdentify<long>
    {
        public virtual void Configure(EntityTypeBuilder<T> builder)
        {
            builder.Property(p => p.Id).HasIdentityOptions();
        }
    }
}