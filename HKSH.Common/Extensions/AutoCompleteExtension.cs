using Newtonsoft.Json;

namespace HKSH.Common.Extensions
{
    /// <summary>
    /// AutoCompleteExtension
    /// </summary>
    public static class AutoCompleteExtension
    {
        /// <summary>
        /// Converts to json.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public static string ToJson<TEntity>(this TEntity model) where TEntity : class => model == null ? string.Empty : JsonConvert.SerializeObject(model);

        /// <summary>
        /// Deseriales the json.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public static TEntity DeserialeJson<TEntity>(this string model) where TEntity : class => string.IsNullOrEmpty(model) ? null : JsonConvert.DeserializeObject<TEntity>(model);

        /// <summary>
        /// Deseriales the json.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="model">The model.</param>
        /// <param name="settings">The settings.</param>
        /// <returns></returns>
        public static TEntity DeserialeJson<TEntity>(this string model, JsonSerializerSettings settings) where TEntity : class => string.IsNullOrEmpty(model) ? null : JsonConvert.DeserializeObject<TEntity>(model, settings);

        /// <summary>
        /// Compares the list.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">The source.</param>
        /// <param name="target">The target.</param>
        /// <returns></returns>
        public static bool CompareList<T>(this List<T> source, List<T> target)
        {
            IEnumerable<T> q = from a in source
                               join b in target on a equals b
                               select a;
            return ((source.Count == target.Count) && (q.Count() == source.Count));
        }
    }
}