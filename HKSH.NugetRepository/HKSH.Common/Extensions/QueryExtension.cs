using System.Linq.Dynamic.Core;
using System.Linq.Expressions;

namespace HKSH.Common.Extensions
{
    /// <summary>
    /// Query Extension
    /// </summary>
    public static class QueryExtension
    {
        /// <summary>
        /// Trues this instance.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static Expression<Func<T, bool>> True<T>() => f => true;

        /// <summary>
        /// Falses this instance.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static Expression<Func<T, bool>> False<T>() => f => false;

        /// <summary>
        /// Ands the specified precidate.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="expression">The expression.</param>
        /// <param name="precidate">The precidate.</param>
        /// <returns></returns>
        public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> expression, Expression<Func<T, bool>> precidate)
        {
            InvocationExpression invokedExpr = Expression.Invoke(precidate, expression.Parameters.Cast<Expression>());
            return Expression.Lambda<Func<T, bool>>(Expression.AndAlso(expression.Body, invokedExpr), expression.Parameters);
        }

        /// <summary>
        /// Ors the specified expression2.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="expression1">The expression1.</param>
        /// <param name="expression2">The expression2.</param>
        /// <returns></returns>
        public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> expression1, Expression<Func<T, bool>> expression2)
        {
            InvocationExpression invokedExpression = Expression.Invoke(expression2, expression1.Parameters.Cast<Expression>());

            return Expression.Lambda<Func<T, bool>>(Expression.Or(expression1.Body, invokedExpression), expression1.Parameters);
        }

        /// <summary>
        /// Wheres if.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">The source.</param>
        /// <param name="condition">if set to <c>true</c> [condition].</param>
        /// <param name="precidate">The precidate.</param>
        /// <returns></returns>
        public static IQueryable<T> WhereIf<T>(this IQueryable<T> source, bool condition, Expression<Func<T, bool>> precidate)
        {
            return condition ? source.Where(precidate) : source;
        }

        /// <summary>
        /// Wheres if.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">The source.</param>
        /// <param name="condition">if set to <c>true</c> [condition].</param>
        /// <param name="predicate">The predicate.</param>
        /// <returns></returns>
        public static IEnumerable<T> WhereIf<T>(this IEnumerable<T> source, bool condition, Func<T, bool> predicate)
        {
            return condition ? source.Where(predicate) : source;
        }

        /// <summary>
        /// Lefts the join.
        /// </summary>
        /// <typeparam name="TLeft">The type of the left.</typeparam>
        /// <typeparam name="TRight">The type of the right.</typeparam>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <param name="source">The source.</param>
        /// <param name="rights">The rights.</param>
        /// <param name="leftKeySelector">The left key selector.</param>
        /// <param name="rightKeySelector">The right key selector.</param>
        /// <returns></returns>
        public static IQueryable<JoinInfo<TLeft, TRight>> LeftJoin<TLeft, TRight, TKey>(this IQueryable<TLeft> source, IQueryable<TRight> rights, Expression<Func<TLeft, TKey>> leftKeySelector, Expression<Func<TRight, TKey>> rightKeySelector)
        {
            return source.GroupJoin(rights, leftKeySelector, rightKeySelector, (left, rights) => new { Left = left, Rights = rights }).SelectMany(result => result.Rights.DefaultIfEmpty(), (result, right) => new JoinInfo<TLeft, TRight> { Left = result.Left, Right = right });
        }

        /// <summary>
        /// Lefts the join.
        /// </summary>
        /// <typeparam name="TLeft">The type of the left.</typeparam>
        /// <typeparam name="TRight">The type of the right.</typeparam>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <param name="source">The source.</param>
        /// <param name="rights">The rights.</param>
        /// <param name="leftKeySelector">The left key selector.</param>
        /// <param name="rightKeySelector">The right key selector.</param>
        /// <returns></returns>
        public static IEnumerable<JoinInfo<TLeft, TRight>> LeftJoin<TLeft, TRight, TKey>(this IEnumerable<TLeft> source, IEnumerable<TRight> rights, Func<TLeft, TKey> leftKeySelector, Func<TRight, TKey> rightKeySelector)
        {
            return source.GroupJoin(rights, leftKeySelector, rightKeySelector, (left, rights) => new { Left = left, Rights = rights }).SelectMany(result => result.Rights.DefaultIfEmpty(), (result, right) => new JoinInfo<TLeft, TRight> { Left = result.Left, Right = right });
        }

        //add by zhx20220922:
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

        /// <summary>
        /// 按pagingRequest分页查询列表数据，返回数据列表
        /// </summary>
        /// <typeparam name="TSource">The type of the source.</typeparam>
        /// <param name="query">The query.</param>
        /// <param name="pageindex">The pageindex.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns></returns>
        public static List<TSource> GetPagingList<TSource>(this IQueryable<TSource> query, int pageindex, int pageSize) where TSource : class
        {
            return query.Skip(pageindex * pageSize).Take(pageSize).ToList();
        }

        /// <summary>
        /// Gets the top list. Default Return 20 rows
        /// </summary>
        /// <typeparam name="TSource">The type of the source.</typeparam>
        /// <param name="query">The query.</param>
        /// <param name="top">The top.</param>
        /// <returns></returns>
        public static List<TSource> GetTopList<TSource>(this IQueryable<TSource> query, int top = 20) where TSource : class
        {
            return query.Take(top).ToList();
        }
    }

    /// <summary>
    /// Join info
    /// </summary>
    /// <typeparam name="TLeft">The type of the left.</typeparam>
    /// <typeparam name="TRight">The type of the right.</typeparam>
    public class JoinInfo<TLeft, TRight>
    {
        /// <summary>
        /// Gets or sets the left.
        /// </summary>
        /// <value>
        /// The left.
        /// </value>
        public TLeft? Left { get; set; }

        /// <summary>
        /// Gets or sets the right.
        /// </summary>
        /// <value>
        /// The right.
        /// </value>
        public TRight? Right { get; set; }
    }
}