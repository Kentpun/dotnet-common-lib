using HKSH.Common.Helper;
using System.Linq.Expressions;
using System.Text;

namespace HKSH.Common.Extensions
{
    /// <summary>
    /// IEnumerable Extension
    /// </summary>
    public static class IEnumerableExtension
    {
        /// <summary>
        /// Orders the by cn.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">The source.</param>
        /// <param name="field">The field.</param>
        /// <returns></returns>
        public static IOrderedEnumerable<T> OrderByCN<T>(this IEnumerable<T> source, Expression<Func<T, string>> field)
        {
            var encoding = Encoding.GetEncoding("gb2312");
            var propertyName = ExpressionHelpers.GetExpressionName(field);
            var property = typeof(T).GetProperty(propertyName);
            return source.OrderBy(o => BitConverter.ToString(encoding.GetBytes(Convert.ToString(property?.GetValue(o))!)));
        }

        /// <summary>
        /// HasValue
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static bool HasValue<T>(this IEnumerable<T>? source) => source != null && source.Any();
    }
}