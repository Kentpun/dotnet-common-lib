using System.Linq.Expressions;
using System.Reflection;

namespace HKSH.Common.Helper
{
    /// <summary>
    /// Expression helpers
    /// </summary>
    public static class ExpressionHelpers
    {
        /// <summary>
        /// Gets the property name of the expression.
        /// </summary>
        /// <typeparam name="T">Any type</typeparam>
        /// <typeparam name="TProp">The type of the prop.</typeparam>
        /// <param name="expression">The expression.</param>
        /// <returns>property name</returns>
        public static string GetExpressionName<T, TProp>(Expression<Func<T, TProp>> expression)
        {
            var propertyInfo = GetExpressionProperty(expression);
            return propertyInfo.Name;
        }

        /// <summary>
        /// Gets the property Info of the expression.
        /// </summary>
        /// <typeparam name="T">Any type</typeparam>
        /// <typeparam name="TProp">The type of the prop.</typeparam>
        /// <param name="expression">The expression.</param>
        /// <returns>property name</returns>
        public static PropertyInfo GetExpressionProperty<T, TProp>(Expression<Func<T, TProp>> expression)
        {
            MemberExpression memberExpression = null;

            if (expression.Body.NodeType == ExpressionType.Convert)
            {
                memberExpression = ((UnaryExpression)expression.Body).Operand as MemberExpression;
            }
            else if (expression.Body.NodeType == ExpressionType.MemberAccess)
            {
                memberExpression = expression.Body as MemberExpression;
            }

            if (memberExpression == null)
            {
                throw new ArgumentException("Not a member access", "expression");
            }

            var propertyInfo = (PropertyInfo)memberExpression.Member;
            return propertyInfo;
        }
    }
}
