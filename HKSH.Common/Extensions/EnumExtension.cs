using System.ComponentModel;
using System.Reflection;

namespace HKSH.Common.Extensions
{
    /// <summary>
    /// Enum extension
    /// </summary>
    public static class EnumExtension
    {
        /// <summary>
        /// Gets the description.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static string? GetDescription(this Enum value) => value.GetType().GetMember(value.ToString()).FirstOrDefault()?.GetCustomAttribute<DescriptionAttribute>()?.Description;

        /// <summary>
        /// Gets the enum value from description.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="description">The description.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentException"></exception>
        public static T GetEnumValueFromDescription<T>(string description)
        {
            var type = typeof(T);
            if (!type.IsEnum)
                throw new ArgumentException($"{type.Name} is not enum type");

            FieldInfo[] fields = type.GetFields();
            var field = fields.SelectMany(f => f.GetCustomAttributes(
                                typeof(DescriptionAttribute), false), (
                                    f, a) => new
                                    {
                                        Field = f,
                                        Att = a
                                    }).SingleOrDefault(a => ((DescriptionAttribute)a.Att)
                                .Description == description);
            return (T)field?.Field.GetRawConstantValue()!;
        }
    }
}