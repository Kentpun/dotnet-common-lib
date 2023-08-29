using HKSH.Common.Base;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Reflection;

namespace HKSH.Common.Configurations
{
    /// <summary>
    /// Use fluent api to alter schema
    /// </summary>
    public static partial class FluentAlterSchema
    {
        /// <summary>
        /// Sets the query filter.
        /// </summary>
        /// <param name="modelBuilder">The model builder.</param>
        public static void SetQueryFilter(ModelBuilder modelBuilder)
        {
            var valueExpr = Expression.Constant(0);
            var allEntities = Assembly.GetEntryAssembly()?.DefinedTypes.Where(a => typeof(IEntityDelTracker).IsAssignableFrom(a));
            if (allEntities != null)
            {
                foreach (var item in allEntities)
                {
                    if (item.BaseType == typeof(BaseTrackedEntity) || item.BaseType == typeof(BaseTrackedEntity<long>))
                    {
                        var typeExpr = Expression.Parameter(item, "entity");
                        var propertyExpr = Expression.Property(typeExpr, "RecordStatus");
                        var equalExpr = Expression.Equal(propertyExpr, valueExpr);
                        var expression = Expression.Lambda(equalExpr, typeExpr);
                        modelBuilder.Entity(item).HasQueryFilter(expression);
                    }
                }
            }
        }
    }
}