namespace HKSH.Common.ShareModel
{
    /// <summary>
    /// object list pair
    /// </summary>
    /// <typeparam name="Key"></typeparam>
    /// <typeparam name="Value"></typeparam>
    public class ObjectListPair<Key, Value> where Key : class? where Value : class?
    {
        /// <summary>
        /// key
        /// </summary>
        public Key? PairKey { get; set; }

        /// <summary>
        /// value
        /// </summary>
        public List<Value>? PairValue { get; set; } = new();

        /// <summary>
        /// 是否只包含key
        /// </summary>
        /// <returns></returns>
        public bool OnlyKey()
        {
            return PairKey != null && PairValue.Count == 0;
        }

        /// <summary>
        /// 是否同時包含key和value
        /// </summary>
        /// <returns></returns>
        public bool Full()
        {
            return !OnlyKey();
        }

        /// <summary>
        /// 匹配對象並返回鍵值對列表
        /// </summary>
        /// <param name="keySet"></param>
        /// <param name="valueSet"></param>
        /// <param name="matcher"></param>
        /// <returns>list</returns>
        public static List<ObjectListPair<Key, Value>> From(List<Key>? keySet, List<Value>? valueSet,
            Func<Key, Value, bool> matcher)
        {
            keySet ??= new List<Key>();
            valueSet ??= new List<Value>();

            keySet = new List<Key>(keySet);
            valueSet = new List<Value>(valueSet);

            var pairs1 = keySet.Select(key => new ObjectListPair<Key, Value>
            {
                PairKey = key,
                PairValue = FindAllAndDelete(valueSet, key, matcher)
            }).ToList();

            return pairs1;
        }

        /// <summary>
        /// 構建一對多
        /// </summary>
        /// <param name="valueSet"></param>
        /// <param name="key"></param>
        /// <param name="matcher"></param>
        /// <returns></returns>
        private static List<Value> FindAllAndDelete(List<Value>? valueSet, Key key, Func<Key, Value, bool> matcher)
        {
            List<Value> result = new();

            for (var i = valueSet.Count - 1; i > -1; i--)
            {
                var value = valueSet[i];
                if (matcher.Invoke(key, value))
                {
                    valueSet.RemoveAt(i);
                    result.Add(value);
                }
            }

            return result;
        }
    }
}