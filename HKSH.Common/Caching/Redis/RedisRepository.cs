using HKSH.Common.Enums.Redis;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace HKSH.Common.Caching.Redis
{
    /// <summary>
    /// Redis Repository
    /// </summary>
    public sealed class RedisRepository : IRedisRepository
    {
        /// <summary>
        /// The lazy connection
        /// </summary>
        private readonly Lazy<ConnectionMultiplexer>? _lazyConnection;

        /// <summary>
        /// The locker
        /// </summary>
        private static readonly object _locker = new();

        /// <summary>
        /// Gets or sets the database.
        /// </summary>
        /// <value>
        /// The database.
        /// </value>
        private IDatabase Db { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="RedisRepository"/> class.
        /// </summary>
        /// <param name="options">The options.</param>
        public RedisRepository(IOptions<RedisOptions> options)
        {
            if (_lazyConnection == null)
            {
                lock (_locker)
                {
                    ConfigurationOptions option = new()
                    {
                        Password = options.Value.Password,
                        AbortOnConnectFail = false
                    };

                    options.Value.EndPoints.ForEach(endPoint =>
                    {
                        option.EndPoints.Add(endPoint, options.Value.Port);
                    });

                    _lazyConnection = new Lazy<ConnectionMultiplexer>(() => ConnectionMultiplexer.Connect(option));
                }
            }

            Db = _lazyConnection.Value.GetDatabase();
        }

        /// <summary>
        /// Gets the database.
        /// </summary>
        /// <value>
        /// The database.
        /// </value>
        public IDatabase Database => Db;

        #region String

        /// <summary>
        /// Sets the string.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <param name="expiry">The expiry.</param>
        /// <returns></returns>
        public bool SetString(string key, string value, TimeSpan? expiry = null) => Db.StringSet(key, value, expiry);

        /// <summary>
        /// Sets the string.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <param name="expiry">The expiry.</param>
        /// <returns></returns>
        public bool SetString(RedisKeys key, string value, TimeSpan? expiry = null) => SetString(key.ToString(), value, expiry);

        /// <summary>
        /// Sets the string.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <param name="expiry">The expiry.</param>
        /// <returns></returns>
        public bool SetString(string key, object value, TimeSpan? expiry = null)
        {
            string json = JsonConvert.SerializeObject(value);
            return SetString(key, json, expiry);
        }

        /// <summary>
        /// Sets the string.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <param name="expiry">The expiry.</param>
        /// <returns></returns>
        public bool SetString(RedisKeys key, object value, TimeSpan? expiry = null) => SetString(key.ToString(), value, expiry);

        /// <summary>
        /// Gets the string.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public string GetString(string key) => Db.StringGet(key);

        /// <summary>
        /// Gets the string.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public string GetString(RedisKeys key) => GetString(key.ToString());

        /// <summary>
        /// Gets the string.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public T GetString<T>(string key)
        {
            string value = GetString(key);
            return string.IsNullOrEmpty(value) ? default : JsonConvert.DeserializeObject<T>(value);
        }

        /// <summary>
        /// Gets the string.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public T GetString<T>(RedisKeys key) => GetString<T>(key.ToString());

        #endregion String

        #region String Async

        /// <summary>
        /// Sets the string.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <param name="expiry">The expiry.</param>
        /// <returns></returns>
        public async Task<bool> SetStringAsync(string key, string value, TimeSpan? expiry = null) => await Db.StringSetAsync(key, value, expiry);

        /// <summary>
        /// Sets the string.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <param name="expiry">The expiry.</param>
        /// <returns></returns>
        public async Task<bool> SetStringAsync(RedisKeys key, string value, TimeSpan? expiry = null) => await SetStringAsync(key.ToString(), value, expiry);

        /// <summary>
        /// Sets the string.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <param name="expiry">The expiry.</param>
        /// <returns></returns>
        public async Task<bool> SetStringAsync(string key, object value, TimeSpan? expiry = null)
        {
            string json = JsonConvert.SerializeObject(value);
            return await SetStringAsync(key, json, expiry);
        }

        /// <summary>
        /// Sets the string.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <param name="expiry">The expiry.</param>
        /// <returns></returns>
        public async Task<bool> SetStringAsync(RedisKeys key, object value, TimeSpan? expiry = null) => await SetStringAsync(key.ToString(), value, expiry);

        /// <summary>
        /// Gets the string.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public async Task<string> GetStringAsync(string key) => await Db.StringGetAsync(key);

        /// <summary>
        /// Gets the string.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public async Task<string> GetStringAsync(RedisKeys key) => await GetStringAsync(key.ToString());

        /// <summary>
        /// Gets the string.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public async Task<T> GetStringAsync<T>(string key)
        {
            string value = await GetStringAsync(key);
            return string.IsNullOrEmpty(value) ? default : JsonConvert.DeserializeObject<T>(value);
        }

        /// <summary>
        /// Gets the string.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public async Task<T> GetStringAsync<T>(RedisKeys key) => await GetStringAsync<T>(key.ToString());

        #endregion String Async

        #region Hash

        /// <summary>
        /// Determine if the field exists in the hash
        /// </summary>
        /// <param name="key"></param>
        /// <param name="field"></param>
        /// <returns></returns>
        public bool HashExists(RedisKeys key, string field)
        {
            return Db.HashExists(key.ToString(), field);
        }

        /// <summary>
        /// Determine if the field exists in the hash
        /// </summary>
        /// <param name="key"></param>
        /// <param name="field"></param>
        /// <returns></returns>
        public bool HashExists(string key, string field)
        {
            return Db.HashExists(key, field);
        }

        /// <summary>
        /// Removes the specified field from the hash
        /// </summary>
        /// <param name="key"></param>
        /// <param name="field"></param>
        /// <returns></returns>
        public bool HashDelete(RedisKeys key, string field)
        {
            return Db.HashDelete(key.ToString(), field);
        }

        /// <summary>
        /// Hashes the delete.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="field">The field.</param>
        /// <returns></returns>
        public bool HashDelete(RedisKeys key, RedisFields field)
        {
            return HashDelete(key.ToString(), field.ToString());
        }

        /// <summary>
        /// Removes the specified field from the hash
        /// </summary>
        /// <param name="key"></param>
        /// <param name="field"></param>
        /// <returns></returns>
        public bool HashDelete(string key, string field)
        {
            return Db.HashDelete(key, field);
        }

        /// <summary>
        /// Removes the specified field from the hash
        /// </summary>
        /// <param name="key"></param>
        /// <param name="fields"></param>
        /// <returns></returns>
        public long HashDelete(RedisKeys key, IEnumerable<string> fields)
        {
            return Db.HashDelete(key.ToString(), fields.Select(x => (RedisValue)x).ToArray());
        }

        /// <summary>
        /// Removes the specified field from the hash
        /// </summary>
        /// <param name="key"></param>
        /// <param name="fields"></param>
        /// <returns></returns>
        public long HashDelete(string key, IEnumerable<string> fields)
        {
            return Db.HashDelete(key, fields.Select(x => (RedisValue)x).ToArray());
        }

        /// <summary>
        /// Set the value of a field in the hash
        /// </summary>
        /// <param name="key"></param>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool HashSet(RedisKeys key, string field, string value)
        {
            return Db.HashSet(key.ToString(), field, value);
        }

        /// <summary>
        /// Hashes the set.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="field">The field.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public bool HashSet(RedisKeys key, RedisFields field, string value)
        {
            return HashSet(key.ToString(), field.ToString(), value);
        }

        /// <summary>
        /// Set the value of a field in the hash
        /// </summary>
        /// <param name="key"></param>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool HashSet(string key, string field, string value)
        {
            return Db.HashSet(key, field, value);
        }

        /// <summary>
        /// Set the value of a field in the hash
        /// </summary>
        /// <param name="key"></param>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool HashSet(RedisKeys key, string field, object value)
        {
            string json = JsonConvert.SerializeObject(value);
            return Db.HashSet(key.ToString(), field, json);
        }

        public bool HashSet(RedisKeys key, RedisFields field, object value)
        {
            return HashSet(key.ToString(), field.ToString(), value);
        }

        /// <summary>
        /// Set the value of a field in the hash
        /// </summary>
        /// <param name="key"></param>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool HashSet(string key, string field, object value)
        {
            string json = JsonConvert.SerializeObject(value);
            return Db.HashSet(key, field, json);
        }

        /// <summary>
        /// Get the value in the hash
        /// </summary>
        /// <param name="key"></param>
        /// <param name="field"></param>
        /// <returns></returns>
        public string HashGet(RedisKeys key, string field)
        {
            return Db.HashGet(key.ToString(), field);
        }

        /// <summary>
        /// Hashes the get.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="field">The field.</param>
        /// <returns></returns>
        public string HashGet(RedisKeys key, RedisFields field)
        {
            return HashGet(key.ToString(), field.ToString());
        }

        /// <summary>
        /// Get the value in the hash
        /// </summary>
        /// <param name="key"></param>
        /// <param name="field"></param>
        /// <returns></returns>
        public string HashGet(string key, string field)
        {
            return Db.HashGet(key, field);
        }

        /// <summary>
        /// Get the value from the hash
        /// </summary>
        /// <param name="key"></param>
        /// <param name="field"></param>
        /// <returns></returns>
        public T HashGet<T>(RedisKeys key, string field)
        {
            RedisValue value = Db.HashGet(key.ToString(), field);
            if (string.IsNullOrEmpty(value))
            {
                return default(T);
            }
            return JsonConvert.DeserializeObject<T>(value);
        }

        /// <summary>
        /// Hashes the get.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">The key.</param>
        /// <param name="field">The field.</param>
        /// <returns></returns>
        public T HashGet<T>(RedisKeys key, RedisFields field)
        {
            return HashGet<T>(key.ToString(), field.ToString());
        }

        /// <summary>
        /// Get the value from the hash
        /// </summary>
        /// <param name="key"></param>
        /// <param name="field"></param>
        /// <returns></returns>
        public T HashGet<T>(string key, string field)
        {
            RedisValue value = Db.HashGet(key, field);
            if (string.IsNullOrEmpty(value))
            {
                return default(T);
            }
            return JsonConvert.DeserializeObject<T>(value);
        }

        /// <summary>
        /// Hashes the get.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public IEnumerable<T> HashGet<T>(RedisKeys key)
        {
            List<T> list = new List<T>();
            HashEntry[] entries = Db.HashGetAll(key.ToString());
            foreach (HashEntry entry in entries)
            {
                list.Add(JsonConvert.DeserializeObject<T>(entry.Value));
            }
            return list;
        }

        /// <summary>
        /// Hashes the get.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public IEnumerable<T> HashGet<T>(string key)
        {
            List<T> list = new List<T>();
            HashEntry[] entries = Db.HashGetAll(key);
            foreach (HashEntry entry in entries)
            {
                list.Add(JsonConvert.DeserializeObject<T>(entry.Value));
            }
            return list;
        }

        /// <summary>
        /// Hashes the scan.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="pattern">The pattern.</param>
        /// <returns></returns>
        public IEnumerable<string> HashScan(RedisKeys key, string pattern)
        {
            IEnumerable<HashEntry> entries = Db.HashScan(key.ToString(), pattern);
            return entries.Select(a => a.Value.ToString());
        }

        /// <summary>
        /// Hashes the scan.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="pattern">The pattern.</param>
        /// <returns></returns>
        public IEnumerable<string> HashScan(string key, string pattern)
        {
            IEnumerable<HashEntry> entries = Db.HashScan(key, pattern);
            return entries.Select(a => a.Value.ToString());
        }

        /// <summary>
        /// Hashes the scan.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">The key.</param>
        /// <param name="pattern">The pattern.</param>
        /// <returns></returns>
        public IEnumerable<T> HashScan<T>(RedisKeys key, string pattern)
        {
            List<T> list = new List<T>();
            IEnumerable<HashEntry> entries = Db.HashScan(key.ToString(), pattern);
            foreach (HashEntry entry in entries)
            {
                list.Add(JsonConvert.DeserializeObject<T>(entry.Value));
            }
            return list;
        }

        /// <summary>
        /// Hashes the get.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">The key.</param>
        /// <param name="pattern">The pattern.</param>
        /// <returns></returns>
        public IEnumerable<T> HashScan<T>(string key, string pattern)
        {
            List<T> list = new List<T>();
            IEnumerable<HashEntry> entries = Db.HashScan(key.ToString(), pattern);
            foreach (HashEntry entry in entries)
            {
                list.Add(JsonConvert.DeserializeObject<T>(entry.Value));
            }
            return list;
        }

        #endregion Hash

        #region List

        /// <summary>
        /// Removes and returns the first element stored in the key list
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string ListLeftPop(RedisKeys key)
        {
            return Db.ListLeftPop(key.ToString());
        }

        /// <summary>
        /// Removes and returns the first element stored in the key list
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string ListLeftPop(string key)
        {
            return Db.ListLeftPop(key);
        }

        /// <summary>
        /// Removes and returns the first element stored in the key list
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public T ListLeftPop<T>(RedisKeys key)
        {
            RedisValue value = Db.ListLeftPop(key.ToString());
            if (string.IsNullOrEmpty(value))
            {
                return default(T);
            }
            return JsonConvert.DeserializeObject<T>(value);
        }

        /// <summary>
        /// Removes and returns the first element stored in the key list
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public T ListLeftPop<T>(string key)
        {
            RedisValue value = Db.ListLeftPop(key);
            if (string.IsNullOrEmpty(value))
            {
                return default(T);
            }
            return JsonConvert.DeserializeObject<T>(value);
        }

        /// <summary>
        /// Removes and returns the last element stored in the key list
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string ListRightPop(RedisKeys key)
        {
            return Db.ListRightPop(key.ToString());
        }

        /// <summary>
        /// Removes and returns the last element stored in the key list
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string ListRightPop(string key)
        {
            return Db.ListRightPop(key);
        }

        /// <summary>
        /// Removes and returns the last element stored in the key list
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public T ListRightPop<T>(RedisKeys key)
        {
            RedisValue value = Db.ListRightPop(key.ToString());
            if (string.IsNullOrEmpty(value))
            {
                return default(T);
            }
            return JsonConvert.DeserializeObject<T>(value);
        }

        /// <summary>
        /// Removes and returns the last element stored in the key list
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public T ListRightPop<T>(string key)
        {
            RedisValue value = Db.ListRightPop(key);
            if (string.IsNullOrEmpty(value))
            {
                return default(T);
            }
            return JsonConvert.DeserializeObject<T>(value);
        }

        /// <summary>
        /// Removes the element with the same value on the key specified in the list
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public long ListRemove(RedisKeys key, string value)
        {
            return Db.ListRemove(key.ToString(), value);
        }

        /// <summary>
        /// Removes the element with the same value on the key specified in the list
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public long ListRemove(string key, string value)
        {
            return Db.ListRemove(key, value);
        }

        /// <summary>
        /// Insert a value at the end of the list.If the key does not exist, create it first and then insert the value
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public long ListRightPush(RedisKeys key, string value)
        {
            return Db.ListRightPush(key.ToString(), value);
        }

        /// <summary>
        /// Insert a value at the end of the list.If the key does not exist, create it first and then insert the value
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public long ListRightPush(string key, string value)
        {
            return Db.ListRightPush(key, value);
        }

        /// <summary>
        /// If the key does not exist, create it first and then insert the value
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public long ListRightPush<T>(RedisKeys key, T value)
        {
            string json = JsonConvert.SerializeObject(value);
            return Db.ListRightPush(key.ToString(), json);
        }

        /// <summary>
        /// If the key does not exist, create it first and then insert the value
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public long ListRightPush<T>(string key, T value)
        {
            string json = JsonConvert.SerializeObject(value);
            return Db.ListRightPush(key, json);
        }

        /// <summary>
        /// If the key does not exist, create it first and then insert the value
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public long ListLeftPush(RedisKeys key, string value)
        {
            return Db.ListLeftPush(key.ToString(), value);
        }

        /// <summary>
        /// If the key does not exist, create it first and then insert the value
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public long ListLeftPush(string key, string value)
        {
            return Db.ListLeftPush(key, value);
        }

        /// <summary>
        /// Inserts a value at the head of the list.If the key does not exist, create it first and then insert the value
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public long ListLeftPush<T>(RedisKeys key, T value)
        {
            string json = JsonConvert.SerializeObject(value);
            return Db.ListLeftPush(key.ToString(), json);
        }

        /// <summary>
        /// Inserts a value at the head of the list.If the key does not exist, create it first and then insert the value
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public long ListLeftPush<T>(string key, T value)
        {
            string json = JsonConvert.SerializeObject(value);
            return Db.ListLeftPush(key, json);
        }

        /// <summary>
        /// Returns the length of the key in the list, or 0 if none exists
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public long ListLength(RedisKeys key)
        {
            return Db.ListLength(key.ToString());
        }

        /// <summary>
        /// Returns the length of the key in the list, or 0 if none exists
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public long ListLength(string key)
        {
            return Db.ListLength(key);
        }

        #endregion List

        #region SortedSet

        /// <summary>
        /// Add sortedSet
        /// </summary>
        /// <param name="key"></param>
        /// <param name="member"></param>
        /// <param name="score"></param>
        /// <returns></returns>
        public bool SortedSetAdd(RedisKeys key, string member, double score)
        {
            return Db.SortedSetAdd(key.ToString(), member, score);
        }

        /// <summary>
        /// Add sortedSet
        /// </summary>
        /// <param name="key"></param>
        /// <param name="member"></param>
        /// <param name="score"></param>
        /// <returns></returns>
        public bool SortedSetAdd(string key, string member, double score)
        {
            return Db.SortedSetAdd(key, member, score);
        }

        /// <summary>
        /// Add sortedSet
        /// </summary>
        /// <param name="key"></param>
        /// <param name="member"></param>
        /// <param name="score"></param>
        /// <returns></returns>
        public bool SortedSetAdd<T>(RedisKeys key, T member, double score)
        {
            string json = JsonConvert.SerializeObject(member);
            return Db.SortedSetAdd(key.ToString(), json, score);
        }

        /// <summary>
        /// Add sortedSet
        /// </summary>
        /// <param name="key"></param>
        /// <param name="member"></param>
        /// <param name="score"></param>
        /// <returns></returns>
        public bool SortedSetAdd<T>(string key, T member, double score)
        {
            string json = JsonConvert.SerializeObject(member);
            return Db.SortedSetAdd(key, json, score);
        }

        /// <summary>
        /// Returns a specified range of elements in an ordered collection, from low to high by default.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="start"></param>
        /// <param name="stop"></param>
        /// <param name="order"></param>
        /// <returns></returns>
        public IEnumerable<string> SortedSetRangeByRank(RedisKeys key, long start = 0L, long stop = -1L, Order order = Order.Ascending)
        {
            return Db.SortedSetRangeByRank(key.ToString(), start, stop, order).Select(x => x.ToString());
        }

        /// <summary>
        /// Returns a specified range of elements in an ordered collection, from low to high by default.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="start"></param>
        /// <param name="stop"></param>
        /// <param name="order"></param>
        /// <returns></returns>
        public IEnumerable<string> SortedSetRangeByRank(string key, long start = 0L, long stop = -1L, Order order = Order.Ascending)
        {
            return Db.SortedSetRangeByRank(key, start, stop, order).Select(x => x.ToString());
        }

        /// <summary>
        /// Returns the number of elements in the ordered collection
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public long SortedSetLength(RedisKeys key)
        {
            return Db.SortedSetLength(key.ToString());
        }

        /// <summary>
        /// Returns the number of elements in the ordered collection
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public long SortedSetLength(string key)
        {
            return Db.SortedSetLength(key);
        }

        /// <summary>
        /// Returns the number of elements in the ordered collection
        /// </summary>
        /// <param name="key"></param>
        /// <param name="memebr"></param>
        /// <returns></returns>
        public bool SortedSetLength(RedisKeys key, string memebr)
        {
            return Db.SortedSetRemove(key.ToString(), memebr);
        }

        /// <summary>
        /// Returns the number of elements in the ordered collection
        /// </summary>
        /// <param name="key"></param>
        /// <param name="memebr"></param>
        /// <returns></returns>
        public bool SortedSetLength(string key, string memebr)
        {
            return Db.SortedSetRemove(key, memebr);
        }

        /// <summary>
        /// The incremental scores sort the members of the set in the store by the key value key by delta
        /// </summary>
        /// <param name="key"></param>
        /// <param name="member"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public double SortedSetIncrement(RedisKeys key, string member, double value = 1)
        {
            return Db.SortedSetIncrement(key.ToString(), member, value);
        }

        /// <summary>
        /// The incremental scores sort the members of the set in the store by the key value key by delta
        /// </summary>
        /// <param name="key"></param>
        /// <param name="member"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public double SortedSetIncrement(string key, string member, double value = 1)
        {
            return Db.SortedSetIncrement(key, member, value);
        }

        /// <summary>
        /// SortedSetRangeByScore
        /// </summary>
        /// <param name="key"></param>
        /// <param name="start"></param>
        /// <param name="stop"></param>
        /// <returns></returns>
        public IEnumerable<string> SortedSetRangeByScore(RedisKeys key, double start = double.NegativeInfinity, double stop = double.PositiveInfinity)
        {
            return Db.SortedSetRangeByScore(key.ToString(), start, stop).Select(x => x.ToString());
        }

        /// <summary>
        /// SortedSetRangeByScore
        /// </summary>
        /// <param name="key"></param>
        /// <param name="start"></param>
        /// <param name="stop"></param>
        /// <returns></returns>
        public IEnumerable<string> SortedSetRangeByScore(string key, double start = double.NegativeInfinity, double stop = double.PositiveInfinity)
        {
            return Db.SortedSetRangeByScore(key, start, stop).Select(x => x.ToString());
        }

        /// <summary>
        /// SortedSetScan
        /// </summary>
        /// <param name="key"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        public IEnumerable<SortedSetEntry> SortedSetScan(RedisKeys key, string pattern)
        {
            return Db.SortedSetScan(key.ToString(), pattern);
        }

        /// <summary>
        /// SortedSetScan
        /// </summary>
        /// <param name="key"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        public IEnumerable<SortedSetEntry> SortedSetScan(string key, string pattern)
        {
            return Db.SortedSetScan(key, pattern);
        }

        /// <summary>
        /// SortedSetRemoveRangeByScore
        /// </summary>
        /// <param name="key"></param>
        /// <param name="start"></param>
        /// <param name="stop"></param>
        public long SortedSetRemoveRangeByScore(RedisKeys key, double start, double stop)
        {
            return Db.SortedSetRemoveRangeByScore(key.ToString(), start, stop);
        }

        /// <summary>
        /// SortedSetRemoveRangeByScore
        /// </summary>
        /// <param name="key"></param>
        /// <param name="start"></param>
        /// <param name="stop"></param>
        public long SortedSetRemoveRangeByScore(string key, double start, double stop)
        {
            return Db.SortedSetRemoveRangeByScore(key, start, stop);
        }

        /// <summary>
        /// SortedSetRemove
        /// </summary>
        /// <param name="key"></param>
        /// <param name="member"></param>
        /// <returns></returns>
        public long SortedSetRemove(RedisKeys key, params RedisValue[] member)
        {
            return Db.SortedSetRemove(key.ToString(), member);
        }

        /// <summary>
        /// SortedSetRemove
        /// </summary>
        /// <param name="key"></param>
        /// <param name="member"></param>
        /// <returns></returns>
        public long SortedSetRemove(string key, params RedisValue[] member)
        {
            return Db.SortedSetRemove(key, member);
        }

        #endregion SortedSet

        #region Key

        /// <summary>
        /// Remove the specified key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool KeyDelete(string key) => Db.KeyDelete(key);

        /// <summary>
        /// Remove the specified key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool KeyDelete(RedisKeys key) => KeyDelete(key.ToString());

        /// <summary>
        /// Remove the specified keys
        /// </summary>
        /// <param name="keys"></param>
        /// <returns></returns>
        public long KeyDelete(IEnumerable<string> keys)
        {
            IEnumerable<RedisKey> s = keys.Select(x => (RedisKey)x);
            return Db.KeyDelete(s.ToArray());
        }

        /// <summary>
        /// Remove the specified keys
        /// </summary>
        /// <param name="keys"></param>
        /// <returns></returns>
        public long KeyDelete(IEnumerable<RedisKeys> keys)
        {
            IEnumerable<RedisKey> s = keys.Select(x => (RedisKey)(x.ToString()));
            return Db.KeyDelete(s.ToArray());
        }

        /// <summary>
        /// Verify that the Key exists
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool KeyExists(string key) => Db.KeyExists(key);

        /// <summary>
        /// Verify that the Key exists
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool KeyExists(RedisKeys key) => KeyExists(key.ToString());

        /// <summary>
        /// Set the expiration time of the Key
        /// </summary>
        /// <param name="key"></param>
        /// <param name="expiry"></param>
        /// <returns></returns>
        public bool KeyExpire(string key, TimeSpan? expiry) => Db.KeyExpire(key, expiry);

        /// <summary>
        /// Set the expiration time of the Key
        /// </summary>
        /// <param name="key"></param>
        /// <param name="expiry"></param>
        /// <returns></returns>
        public bool KeyExpire(RedisKeys key, TimeSpan? expiry) => KeyExpire(key.ToString(), expiry);

        #endregion Key

        #region Key Async

        /// <summary>
        /// Remove the specified key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<bool> KeyDeleteAsync(string key) => await Db.KeyDeleteAsync(key);

        /// <summary>
        /// Remove the specified key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<bool> KeyDeleteAsync(RedisKeys key) => await KeyDeleteAsync(key.ToString());

        /// <summary>
        /// Remove the specified keys
        /// </summary>
        /// <param name="keys"></param>
        /// <returns></returns>
        public async Task<long> KeyDeleteAsync(IEnumerable<string> keys)
        {
            IEnumerable<RedisKey> s = keys.Select(x => (RedisKey)x);
            return await Db.KeyDeleteAsync(s.ToArray());
        }

        /// <summary>
        /// Remove the specified keys
        /// </summary>
        /// <param name="keys"></param>
        /// <returns></returns>
        public async Task<long> KeyDeleteAsync(IEnumerable<RedisKeys> keys)
        {
            IEnumerable<RedisKey> s = keys.Select(x => (RedisKey)(x.ToString()));
            return await Db.KeyDeleteAsync(s.ToArray());
        }

        /// <summary>
        /// Verify that the Key exists
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<bool> KeyExistsAsync(string key) => await Db.KeyExistsAsync(key);

        /// <summary>
        /// Verify that the Key exists
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<bool> KeyExistsAsync(RedisKeys key) => await KeyExistsAsync(key.ToString());

        /// <summary>
        /// Set the expiration time of the Key
        /// </summary>
        /// <param name="key"></param>
        /// <param name="expiry"></param>
        /// <returns></returns>
        public async Task<bool> KeyExpireAsync(string key, TimeSpan? expiry) => await Db.KeyExpireAsync(key, expiry);

        /// <summary>
        /// Set the expiration time of the Key
        /// </summary>
        /// <param name="key"></param>
        /// <param name="expiry"></param>
        /// <returns></returns>
        public async Task<bool> KeyExpireAsync(RedisKeys key, TimeSpan? expiry) => await KeyExpireAsync(key.ToString(), expiry);

        #endregion Key Async

        #region Lock Async

        /// <summary>
        /// Locks the take asynchronous.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <param name="expiry">The expiry. Default 10 minutes.</param>
        /// <returns></returns>
        public async Task<bool> LockTakeAsync(RedisLockKeys key, string value, TimeSpan? expiry = null) => await LockTakeAsync(key.ToString(), value, expiry);

        /// <summary>
        /// Locks the take asynchronous.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <param name="expiry">The expiry. Default 10 minutes.</param>
        /// <returns></returns>
        public async Task<bool> LockTakeAsync(string key, string value, TimeSpan? expiry = null) => await Db.LockTakeAsync(key, value, expiry ?? TimeSpan.FromMinutes(10));

        /// <summary>
        /// Locks the release asynchronous.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public async Task<bool> LockReleaseAsync(RedisLockKeys key, string value) => await LockReleaseAsync(key.ToString(), value);

        /// <summary>
        /// Locks the release asynchronous.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public async Task<bool> LockReleaseAsync(string key, string value) => await Db.LockReleaseAsync(key, value);

        /// <summary>
        /// Hashes the fields.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public IEnumerable<string> HashFields(string key)
        {
            List<string> list = new List<string>();

            RedisValue[] fields = Db.HashKeys(key);

            foreach (RedisValue item in fields)
            {
                list.Add(item.ToString());
            }

            return list;
        }

        #endregion Lock Async
    }
}