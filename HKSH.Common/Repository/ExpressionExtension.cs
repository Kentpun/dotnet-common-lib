using System.Collections.ObjectModel;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Reflection;

namespace HKSH.Common.Repository
{
    /// <summary>
    /// Query
    /// </summary>
    public class Query
    {
        /// <summary>
        /// Operators
        /// </summary>
        public enum Operators
        {
            /// <summary>
            /// The none
            /// </summary>
            None = 0,

            /// <summary>
            /// The equal
            /// </summary>
            Equal = 1,

            /// <summary>
            /// The greater than
            /// </summary>
            GreaterThan = 2,

            /// <summary>
            /// The greater than or equal
            /// </summary>
            GreaterThanOrEqual = 3,

            /// <summary>
            /// The less than
            /// </summary>
            LessThan = 4,

            /// <summary>
            /// The less than or equal
            /// </summary>
            LessThanOrEqual = 5,

            /// <summary>
            /// Determines whether this instance contains the object.
            /// </summary>
            Contains = 6,

            /// <summary>
            /// The start with
            /// </summary>
            StartWith = 7,

            /// <summary>
            /// The end width
            /// </summary>
            EndWidth = 8,

            /// <summary>
            /// The range
            /// </summary>
            Range = 9
        }

        /// <summary>
        /// Condition
        /// </summary>
        public enum Condition
        {
            /// <summary>
            /// The or else
            /// </summary>
            OrElse = 1,

            /// <summary>
            /// The and also
            /// </summary>
            AndAlso = 2
        }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the operator.
        /// </summary>
        /// <value>
        /// The operator.
        /// </value>
        public Operators Operator { get; set; }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        public object Value { get; set; } = null!;

        /// <summary>
        /// Gets or sets the value minimum.
        /// </summary>
        /// <value>
        /// The value minimum.
        /// </value>
        public object ValueMin { get; set; } = null!;

        /// <summary>
        /// Gets or sets the value maximum.
        /// </summary>
        /// <value>
        /// The value maximum.
        /// </value>
        public object ValueMax { get; set; } = null!;
    }

    /// <summary>
    /// QueryCollection
    /// </summary>
    public class QueryCollection : Collection<Query>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="QueryCollection" /> class.
        /// </summary>
        public QueryCollection()
        {
            Add(new Query { Name = "RecordStatus", Operator = Query.Operators.Equal, Value = 0 });
        }

        /// <summary>
        /// Ases the expression.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="condition">The condition.</param>
        /// <returns></returns>
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

    /// <summary>
    /// Query Extensions
    /// </summary>
    public static class QueryExtensions
    {
        /// <summary>
        /// Orders the specified order.
        /// </summary>
        /// <typeparam name="TSource">The type of the source.</typeparam>
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