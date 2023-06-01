using HKSH.Common.Helper;
using System.Linq.Expressions;
using System.Text;

namespace ABI.Artwork.Utils.Extensions
{
    /// <summary>
    /// IEnumerableExtension
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
            return source.OrderBy(o => BitConverter.ToString(encoding.GetBytes(Convert.ToString(property.GetValue(o)))));
        }
    }
}
