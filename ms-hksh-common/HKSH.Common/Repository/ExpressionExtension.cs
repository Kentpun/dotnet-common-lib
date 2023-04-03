using System.Collections.ObjectModel;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Reflection;

namespace HKSH.Common.Repository
{
    public class Query
    {
        public enum Operators
        {
            None = 0,
            Equal = 1,
            GreaterThan = 2,
            GreaterThanOrEqual = 3,
            LessThan = 4,
            LessThanOrEqual = 5,
            Contains = 6,
            StartWith = 7,
            EndWidth = 8,
            Range = 9
        }

        public enum Condition
        {
            OrElse = 1,
            AndAlso = 2
        }

        public string Name { get; set; }

        public Operators Operator { get; set; }

        public object Value { get; set; }

        public object ValueMin { get; set; }

        public object ValueMax { get; set; }
    }

    public class QueryCollection : Collection<Query>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="QueryCollection"/> class.
        /// </summary>
        public QueryCollection()
        {
            Add(new Query { Name = "IsDeleted", Operator = Query.Operators.Equal, Value = false });
        }

        public Expression<Func<T, bool>> AsExpression<T>(Query.Condition? condition = Query.Condition.AndAlso) where T : class
        {
            Type targetType = typeof(T);
            TypeInfo typeInfo = targetType.GetTypeInfo();
            ParameterExpression parameter = Expression.Parameter(targetType, "m");
            Expression expression = null;
            Func<Expression, Expression, Expression> Append = (exp1, exp2) =>
            {
                if (exp1 == null)
                {
                    return exp2;
                }
                if ((condition ?? Query.Condition.OrElse) == Query.Condition.OrElse)
                {
                    return Expression.OrElse(exp1, exp2);
                }
                return Expression.AndAlso(exp1, exp2);
            };
            foreach (Query item in this)
            {
                PropertyInfo property = typeInfo.GetProperty(item.Name);
                if (property == null ||
                    !property.CanRead ||
                    (item.Operator != Query.Operators.Range && item.Value == null) ||
                    (item.Operator == Query.Operators.Range && item.ValueMin == null && item.ValueMax == null))
                {
                    continue;
                }
                Type realType = Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType;
                if (item.Value != null)
                {
                    item.Value = Convert.ChangeType(item.Value, realType);
                }
                Expression<Func<object>> valueLamba = () => item.Value;
                switch (item.Operator)
                {
                    case Query.Operators.Equal:
                        {
                            expression = Append(expression, Expression.Equal(Expression.Property(parameter, item.Name),
                                Expression.Convert(valueLamba.Body, property.PropertyType)));
                            break;
                        }
                    case Query.Operators.GreaterThan:
                        {
                            expression = Append(expression, Expression.GreaterThan(Expression.Property(parameter, item.Name),
                                Expression.Convert(valueLamba.Body, property.PropertyType)));
                            break;
                        }
                    case Query.Operators.GreaterThanOrEqual:
                        {
                            expression = Append(expression, Expression.GreaterThanOrEqual(Expression.Property(parameter, item.Name),
                                Expression.Convert(valueLamba.Body, property.PropertyType)));
                            break;
                        }
                    case Query.Operators.LessThan:
                        {
                            expression = Append(expression, Expression.LessThan(Expression.Property(parameter, item.Name),
                                Expression.Convert(valueLamba.Body, property.PropertyType)));
                            break;
                        }
                    case Query.Operators.LessThanOrEqual:
                        {
                            expression = Append(expression, Expression.LessThanOrEqual(Expression.Property(parameter, item.Name),
                                Expression.Convert(valueLamba.Body, property.PropertyType)));
                            break;
                        }
                    case Query.Operators.Contains:
                        {
                            UnaryExpression nullCheck = Expression.Not(Expression.Call(typeof(string), "IsNullOrEmpty", null, Expression.Property(parameter, item.Name)));
                            MethodCallExpression contains = Expression.Call(Expression.Property(parameter, item.Name), "Contains", null,
                                Expression.Convert(valueLamba.Body, property.PropertyType));
                            expression = Append(expression, Expression.AndAlso(nullCheck, contains));
                            break;
                        }
                    case Query.Operators.StartWith:
                        {
                            UnaryExpression nullCheck = Expression.Not(Expression.Call(typeof(string), "IsNullOrEmpty", null, Expression.Property(parameter, item.Name)));
                            MethodCallExpression startsWith = Expression.Call(Expression.Property(parameter, item.Name), "StartsWith", null,
                                Expression.Convert(valueLamba.Body, property.PropertyType));
                            expression = Append(expression, Expression.AndAlso(nullCheck, startsWith));
                            break;
                        }
                    case Query.Operators.EndWidth:
                        {
                            UnaryExpression nullCheck = Expression.Not(Expression.Call(typeof(string), "IsNullOrEmpty", null, Expression.Property(parameter, item.Name)));
                            MethodCallExpression endsWith = Expression.Call(Expression.Property(parameter, item.Name), "EndsWith", null,
                                Expression.Convert(valueLamba.Body, property.PropertyType));
                            expression = Append(expression, Expression.AndAlso(nullCheck, endsWith));
                            break;
                        }
                    case Query.Operators.Range:
                        {
                            Expression minExp = null, maxExp = null;
                            if (item.ValueMin != null)
                            {
                                object minValue = Convert.ChangeType(item.ValueMin, realType);
                                Expression<Func<object>> minValueLamda = () => minValue;
                                minExp = Expression.GreaterThanOrEqual(Expression.Property(parameter, item.Name), Expression.Convert(minValueLamda.Body, property.PropertyType));
                            }
                            if (item.ValueMax != null)
                            {
                                object maxValue = Convert.ChangeType(item.ValueMax, realType);
                                Expression<Func<object>> maxValueLamda = () => maxValue;
                                maxExp = Expression.LessThanOrEqual(Expression.Property(parameter, item.Name), Expression.Convert(maxValueLamda.Body, property.PropertyType));
                            }

                            if (minExp != null && maxExp != null)
                            {
                                expression = Append(expression, Expression.AndAlso(minExp, maxExp));
                            }
                            else if (minExp != null)
                            {
                                expression = Append(expression, minExp);
                            }
                            else if (maxExp != null)
                            {
                                expression = Append(expression, maxExp);
                            }

                            break;
                        }
                }
            }
            return expression == null ? null : (Expression<Func<T, bool>>)Expression.Lambda(expression, parameter);
        }
    }

    public static class QueryExtensions
    {
        /// <summary>
        /// Orders the specified order.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="order">The order.fow example:order="age asc,name desc" or order="age desc"</param>
        /// <returns></returns>
        public static IQueryable<TSource> DynamicOrder<TSource>(this IQueryable<TSource> query, string order) where TSource : class
        {
            if (string.IsNullOrEmpty(order))
            {
                return query;
            }
            // Verify that the field is inside
            var l = order.ToLower();
            if (query.Expression.Type.GetProperties().Any(p => l.Contains(p.Name.ToLower())))
            {
                return query;
            }
            return query.OrderBy(order);
        }
    }
}