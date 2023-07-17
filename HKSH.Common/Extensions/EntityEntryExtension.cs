using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace HKSH.Common.Extensions
{
    /// <summary>
    /// EntityEntry Extension
    /// </summary>
    public static class EntityEntryExtension
    {
        /// <summary>
        /// Primaries the key.
        /// </summary>
        /// <param name="entry">The entry.</param>
        /// <returns></returns>
        public static string PrimaryKey(this EntityEntry entry)
        {
            var key = entry.Metadata.FindPrimaryKey();

            var values = new List<object>();
            foreach (var property in key?.Properties ?? new List<Property>())
            {
                var value = entry.Property(property.Name).CurrentValue;
                if (value != null)
                {
                    values.Add(value);
                }
            }

            return string.Join(",", values);
        }
    }
}