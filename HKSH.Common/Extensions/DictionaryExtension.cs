using Newtonsoft.Json;

namespace HKSH.Common.Extensions
{
    /// <summary>
    /// Dictionary Extension
    /// </summary>
    public static class DictionaryExtension
    {
        /// <summary>
        /// Deserializes the string to dictionary.
        /// </summary>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="json">The json.</param>
        /// <returns></returns>
        public static Dictionary<TKey, TValue> DeserializeStringToDictionary<TKey, TValue>(this string json) where TKey : notnull => string.IsNullOrEmpty(json) ? new Dictionary<TKey, TValue>() : JsonConvert.DeserializeObject<Dictionary<TKey, TValue>>(json)!;
    }
}