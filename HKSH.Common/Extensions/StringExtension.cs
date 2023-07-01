using Nest;
using System.Text;
using System.Text.RegularExpressions;

namespace HKSH.Common.Extensions
{
    /// <summary>
    /// StringExtension
    /// </summary>
    public static class StringExtension
    {
        /// <summary>
        /// Determines whether this instance is missing.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        ///   <c>true</c> if the specified value is missing; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsMissing(this string value)
        {
            return string.IsNullOrWhiteSpace(value);
        }

        /// <summary>
        /// Gets the file extension.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <returns></returns>
        public static string GetFileExtension(this string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName))
            {
                return string.Empty;
            }
            int num = fileName.LastIndexOf('.');
            return num < 0 ? string.Empty : fileName[num..]?.ToLower();
        }

        /// <summary>
        /// Gets the character count.
        /// </summary>
        /// <param name="charString">The character string.</param>
        /// <param name="charCode">The character code.</param>
        /// <returns></returns>
        public static int GetCharCount(this string charString, char charCode) => string.IsNullOrEmpty(charString) ? 0 : charString.ToCharArray().Count(s => s == charCode);

        /// <summary>
        /// Removes the symbol.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns></returns>
        public static string RemoveSymbol(this string input)
        {
            var regex = new Regex(@"((?=[\x21-\x7e\s]+)[^A-Za-z0-9])");
            return regex.Replace(input.ToDBC(), "");
        }

        /// <summary>
        /// Converts to dbc.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns></returns>
        public static string ToDBC(this string input)
        {
            char[] array = input.ToCharArray();
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] == 12288)
                {
                    array[i] = (char)32;
                    continue;
                }
                if (array[i] > 65280 && array[i] < 65375)
                {
                    array[i] = (char)(array[i] - 65248);
                }
            }
            return new string(array);
        }

        /// <summary>
        /// Cuts the off prefix.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="prefix">The prefix.</param>
        /// <param name="comparison">The comparison.</param>
        /// <returns></returns>
        public static string CutOffPrefix(this string input, string prefix, StringComparison comparison = StringComparison.OrdinalIgnoreCase)
        {
            if (string.IsNullOrWhiteSpace(input) || string.IsNullOrWhiteSpace(prefix)
                || !input.StartsWith(prefix, comparison))
            {
                return input;
            }
            return input.Substring(prefix.Length);
        }

        /// <summary>
        /// Cuts the off suffix.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="suffix">The suffix.</param>
        /// <param name="comparison">The comparison.</param>
        /// <returns></returns>
        public static string CutOffSuffix(this string input, string suffix, StringComparison comparison = StringComparison.OrdinalIgnoreCase)
        {
            if (string.IsNullOrWhiteSpace(input) || string.IsNullOrWhiteSpace(suffix)
                || !input.EndsWith(suffix, comparison))
            {
                return input;
            }
            return input.Substring(0, input.LastIndexOf(suffix, comparison));
        }

        /// <summary>
        /// Appends the prefix.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="prefix">The prefix.</param>
        /// <param name="force">if set to <c>true</c> [force].</param>
        /// <param name="comparison">The comparison.</param>
        /// <returns></returns>
        public static string AppendPrefix(this string input, string prefix, bool force = false, StringComparison comparison = StringComparison.OrdinalIgnoreCase)
        {
            if (string.IsNullOrWhiteSpace(input) || string.IsNullOrWhiteSpace(prefix))
            {
                return input;
            }
            if (force)
            {
                return prefix + input;
            }
            else
            {
                return input.StartsWith(prefix, comparison) ? input : prefix + input;
            }
        }

        /// <summary>
        /// Appends the suffix.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="suffix">The suffix.</param>
        /// <param name="force">if set to <c>true</c> [force].</param>
        /// <param name="comparison">The comparison.</param>
        /// <returns></returns>
        public static string AppendSuffix(this string input, string suffix, bool force = false, StringComparison comparison = StringComparison.OrdinalIgnoreCase)
        {
            if (string.IsNullOrWhiteSpace(input) || string.IsNullOrWhiteSpace(suffix))
            {
                return input;
            }
            if (force)
            {
                return input + suffix;
            }
            else
            {
                return input.EndsWith(suffix, comparison) ? input : input + suffix;
            }
        }

        /// <summary>
        /// Units the upper.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="unit">The unit.</param>
        /// <param name="comparison">The comparison.</param>
        /// <returns></returns>
        public static string UnitUpper(this string input, string unit, StringComparison comparison = StringComparison.OrdinalIgnoreCase)
        {
            if (string.IsNullOrWhiteSpace(input) || string.IsNullOrWhiteSpace(unit)
                || !input.EndsWith(unit, comparison))
            {
                return input;
            }
            return input.CutOffSuffix(unit, comparison) + unit.ToUpper();
        }

        /// <summary>
        /// Units the lower.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="unit">The unit.</param>
        /// <param name="comparison">The comparison.</param>
        /// <returns></returns>
        public static string UnitLower(this string input, string unit, StringComparison comparison = StringComparison.OrdinalIgnoreCase)
        {
            if (string.IsNullOrWhiteSpace(input) || string.IsNullOrWhiteSpace(unit)
                || !input.EndsWith(unit, comparison))
            {
                return input;
            }
            return input.CutOffSuffix(unit, comparison) + unit.ToLower();
        }

        /// <summary>
        /// Units the trans.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="unitOld">The unit old.</param>
        /// <param name="unitNew">The unit new.</param>
        /// <param name="comparison">The comparison.</param>
        /// <returns></returns>
        public static string UnitTrans(this string input, string unitOld, string unitNew, StringComparison comparison = StringComparison.OrdinalIgnoreCase)
        {
            if (string.IsNullOrWhiteSpace(input) || string.IsNullOrWhiteSpace(unitOld)
                || !input.EndsWith(unitOld, comparison))
            {
                return input;
            }
            return input.Substring(0, input.LastIndexOf(unitOld, comparison)) + unitNew;
        }

        /// <summary>
        /// Unescapes the unicode.
        /// </summary>
        /// <param name="str">The string.</param>
        /// <returns></returns>
        public static string UnescapeUnicode(this string str)  // 将unicode转义序列(\uxxxx)解码为字符串
        {
            if (string.IsNullOrWhiteSpace(str))
            {
                return str;
            }
            return (System.Text.RegularExpressions.Regex.Unescape(str));
        }

        /// <summary>
        /// Escapes the unicode.
        /// </summary>
        /// <param name="str">The string.</param>
        /// <returns></returns>
        public static string EscapeUnicode(this string str)  // 将字符串编码为unicode转义序列(\uxxxx)
        {
            if (string.IsNullOrWhiteSpace(str))
            {
                return str;
            }
            StringBuilder tmp = new StringBuilder();
            for (int i = 0; i < str.Length; i++)
            {
                ushort uxc = (ushort)str[i];
                tmp.Append(@"\u" + uxc.ToString("x4"));
            }
            return (tmp.ToString());
        }

        /// <summary>
        /// Determines whether the specified sub item contains any.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="subItem">The sub item.</param>
        /// <returns>
        ///   <c>true</c> if the specified sub item contains any; otherwise, <c>false</c>.
        /// </returns>
        public static bool ContainsAny(this List<string> input, List<string> subItem)
        {
            if (input == null || subItem == null ||
                !input.Any() || !subItem.Any())
            {
                return false;
            }
            foreach (var item in subItem)
            {
                if (input.Contains(item))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Deserializes the string to dictionary.
        /// </summary>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="jsonStr">The json string.</param>
        /// <returns></returns>
        public static Dictionary<TKey, TValue>? DeserializeToDictionary<TKey, TValue>(this string? jsonStr)
        {
            if (string.IsNullOrEmpty(jsonStr))
                return new Dictionary<TKey, TValue>();

            return System.Text.Json.JsonSerializer.Deserialize<Dictionary<TKey, TValue>>(jsonStr);
        }

        /// <summary>
        /// Deserializes to tuples.
        /// </summary>
        /// <typeparam name="T1">The type of the 1.</typeparam>
        /// <typeparam name="T2">The type of the 2.</typeparam>
        /// <typeparam name="T3">The type of the 3.</typeparam>
        /// <param name="jsonStr">The json string.</param>
        /// <returns></returns>
        public static List<Tuple<T1, T2, T3>>? DeserializeToTuples<T1, T2, T3>(this string? jsonStr)
        {
            if (string.IsNullOrEmpty(jsonStr))
                return new List<Tuple<T1, T2, T3>>();

            return System.Text.Json.JsonSerializer.Deserialize<List<Tuple<T1, T2, T3>>>(jsonStr);
        }

        /// <summary>
        /// Converts to datetime.
        /// </summary>
        /// <param name="strDatetime">The string datetime.</param>
        /// <returns></returns>
        public static DateTime? ToDateTime(this string? strDatetime)
        {
            if (strDatetime.IsMissing())
            {
                return null;
            }

            if (DateTime.TryParse(strDatetime, out DateTime dt))
            {
                return dt;
            }
            return null;
        }
    }
}