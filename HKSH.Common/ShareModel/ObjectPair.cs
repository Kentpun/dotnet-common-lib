namespace HKSH.Common.ShareModel
{
    /// <summary>
    /// object pair
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    public class ObjectPair<TKey, TValue> where TKey : class? where TValue : class?
    {
        /// <summary>
        /// key
        /// </summary>
        public TKey? PairKey { get; set; }

        /// <summary>
        /// value
        /// </summary>
        public TValue? PairValue { get; set; }

        /// <summary>
        /// is only key
        /// </summary>
        /// <returns></returns>
        public bool OnlyKey()
        {
            return PairKey != null && PairValue == null;
        }

        /// <summary>
        /// is only value
        /// </summary>
        /// <returns></returns>
        public bool OnlyValue()
        {
            return PairKey == null && PairValue != null;
        }

        /// <summary>
        /// has key and value
        /// </summary>
        /// <returns></returns>
        public bool Full()
        {
            return !OnlyKey() && !OnlyValue();
        }

        /// <summary>
        /// compere key and value
        /// </summary>
        /// <param name="comparator"></param>
        /// <returns></returns>
        public bool HasDiff(Func<TKey, TValue, bool> comparator)
        {
            return OnlyKey() || OnlyValue() || Full() && comparator.Invoke(PairKey!, PairValue!);
        }

        /// <summary>
        /// match object pair
        /// </summary>
        /// <param name="keySet"></param>
        /// <param name="valueSet"></param>
        /// <param name="matcher"></param>
        /// <returns>list</returns>
        public static List<ObjectPair<TKey, TValue>> From(List<TKey>? keySet, List<TValue>? valueSet,
            Func<TKey, TValue, bool> matcher)
        {
            keySet ??= new List<TKey>();
            valueSet ??= new List<TValue>();

            keySet = new List<TKey>(keySet);
            valueSet = new List<TValue>(valueSet);

            var pairs1 = keySet.Select(key => new ObjectPair<TKey, TValue>
            {
                PairKey = key,
                PairValue = FindAndDelete(valueSet, key, matcher)
            });

            return pairs1.Union(valueSet.Select(value =>
                new ObjectPair<TKey, TValue>
                {
                    PairKey = null,
                    PairValue = value
                })).ToList();
        }

        /// <summary>
        /// Finds the key match value and delete.
        /// </summary>
        /// <param name="valueSet">The value set.</param>
        /// <param name="key">The key.</param>
        /// <param name="matcher">The matcher.</param>
        /// <returns></returns>
        private static TValue? FindAndDelete(List<TValue> valueSet, TKey key, Func<TKey, TValue, bool> matcher)
        {
            for (var i = valueSet.Count - 1; i > -1; i--)
            {
                var value = valueSet[i];
                if (matcher.Invoke(key, value))
                {
                    valueSet.RemoveAt(i);
                    return value;
                }
            }

            return null;
        }
    }
}