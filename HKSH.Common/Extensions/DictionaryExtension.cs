using Newtonsoft.Json;

namespace HKSH.Common.Extensions
{
    /// <summary>
    /// DictionaryExtension
    /// </summary>
    public static class DictionaryExtension
    {
        /// <summary>
        /// Deserializes the string to dictionary.
        /// </summary>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="jsonStr">The json string.</param>
        /// <returns></returns>
        public static Dictionary<TKey, TValue> DeserializeStringToDictionary<TKey, TValue>(string jsonStr)
        {
            if (string.IsNullOrEmpty(jsonStr))
                return new Dictionary<TKey, TValue>();

            return JsonConvert.DeserializeObject<Dictionary<TKey, TValue>>(jsonStr);
        }
    }
}