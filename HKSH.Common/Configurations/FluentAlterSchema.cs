using HKSH.Common.ShareModel.Base;
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
            ConstantExpression valueExpr = Expression.Constant((byte)0);
            IEnumerable<TypeInfo>? allEntities = Assembly.GetEntryAssembly()?.DefinedTypes.Where(a => typeof(IEntityDelTracker).IsAssignableFrom(a));
            if (allEntities != null)
            {
                foreach (TypeInfo? item in allEntities)
                {
                    if (item.BaseType == typeof(BaseTrackedEntity) || item.BaseType == typeof(BaseTrackedEntity<long>))
                    {
                        ParameterExpression typeExpr = Expression.Parameter(item, "entity");
                        MemberExpression propertyExpr = Expression.Property(typeExpr, "RecordStatus");
                        BinaryExpression equalExpr = Expression.Equal(propertyExpr, valueExpr);
                        LambdaExpression expression = Expression.Lambda(equalExpr, typeExpr);
                        modelBuilder.Entity(item).HasQueryFilter(expression);
                    }
                }
            }
        }
    }
}