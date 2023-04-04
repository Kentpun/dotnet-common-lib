using HKSH.Common.Caching.Redis.Enum;
using HKSH.Utils.Redis;
using StackExchange.Redis;

namespace HKSH.Common.Caching.Redis
{
    /// <summary>
    /// IRedisRepository
    /// </summary>
    public interface IRedisRepository
    {
        /// <summary>
        /// Gets the database.
        /// </summary>
        /// <value>
        /// The database.
        /// </value>
        IDatabase Database { get; }

        #region String

        /// <summary>
        /// Sets the string.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <param name="expiry">The expiry.</param>
        /// <returns></returns>
        bool SetString(string key, string value, TimeSpan? expiry = null);

        /// <summary>
        /// Sets the string.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <param name="expiry">The expiry.</param>
        /// <returns></returns>
        bool SetString(RedisKeys key, string value, TimeSpan? expiry = null);

        /// <summary>
        /// Sets the string.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <param name="expiry">The expiry.</param>
        /// <returns></returns>
        bool SetString(string key, object value, TimeSpan? expiry = null);

        /// <summary>
        /// Sets the string.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <param name="expiry">The expiry.</param>
        /// <returns></returns>
        bool SetString(RedisKeys key, object value, TimeSpan? expiry = null);

        /// <summary>
        /// Gets the string.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        string GetString(string key);

        /// <summary>
        /// Gets the string.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        string GetString(RedisKeys key);

        /// <summary>
        /// Gets the string.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        T GetString<T>(string key);

        /// <summary>
        /// Gets the string.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        T GetString<T>(RedisKeys key);

        #endregion String

        #region String Async

        /// <summary>
        /// Sets the string.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <param name="expiry">The expiry.</param>
        /// <returns></returns>
        Task<bool> SetStringAsync(string key, string value, TimeSpan? expiry = null);

        /// <summary>
        /// Sets the string.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <param name="expiry">The expiry.</param>
        /// <returns></returns>
        Task<bool> SetStringAsync(RedisKeys key, string value, TimeSpan? expiry = null);

        /// <summary>
        /// Sets the string.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <param name="expiry">The expiry.</param>
        /// <returns></returns>
        Task<bool> SetStringAsync(string key, object value, TimeSpan? expiry = null);

        /// <summary>
        /// Sets the string.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <param name="expiry">The expiry.</param>
        /// <returns></returns>
        Task<bool> SetStringAsync(RedisKeys key, object value, TimeSpan? expiry = null);

        /// <summary>
        /// Gets the string.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        Task<string> GetStringAsync(string key);

        /// <summary>
        /// Gets the string.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        Task<string> GetStringAsync(RedisKeys key);

        /// <summary>
        /// Gets the string.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        Task<T> GetStringAsync<T>(string key);

        /// <summary>
        /// Gets the string.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        Task<T> GetStringAsync<T>(RedisKeys key);

        #endregion String Async

        #region Key

        /// <summary>
        /// Keys the delete.
        /// </summary>
        /// <param name="keys">The keys.</param>
        /// <returns></returns>
        long KeyDelete(IEnumerable<string> keys);

        /// <summary>
        /// Keys the delete.
        /// </summary>
        /// <param name="keys">The keys.</param>
        /// <returns></returns>
        long KeyDelete(IEnumerable<RedisKeys> keys);

        /// <summary>
        /// Keys the delete.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        bool KeyDelete(string key);

        /// <summary>
        /// Keys the delete.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        bool KeyDelete(RedisKeys key);

        /// <summary>
        /// Keys the exists.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        bool KeyExists(string key);

        /// <summary>
        /// Keys the exists.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        bool KeyExists(RedisKeys key);

        /// <summary>
        /// Keys the expire.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="expiry">The expiry.</param>
        /// <returns></returns>
        bool KeyExpire(string key, TimeSpan? expiry);

        /// <summary>
        /// Keys the expire.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="expiry">The expiry.</param>
        /// <returns></returns>
        bool KeyExpire(RedisKeys key, TimeSpan? expiry);

        #endregion Key

        #region Key Async

        /// <summary>
        /// Remove the specified key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<bool> KeyDeleteAsync(string key);

        /// <summary>
        /// Remove the specified key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<bool> KeyDeleteAsync(RedisKeys key);

        /// <summary>
        /// Remove the specified keys
        /// </summary>
        /// <param name="keys"></param>
        /// <returns></returns>
        Task<long> KeyDeleteAsync(IEnumerable<string> keys);

        /// <summary>
        /// Remove the specified keys
        /// </summary>
        /// <param name="keys"></param>
        /// <returns></returns>
        Task<long> KeyDeleteAsync(IEnumerable<RedisKeys> keys);

        /// <summary>
        /// Verify that the Key exists
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<bool> KeyExistsAsync(string key);

        /// <summary>
        /// Verify that the Key exists
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<bool> KeyExistsAsync(RedisKeys key);

        /// <summary>
        /// Set the expiration time of the Key
        /// </summary>
        /// <param name="key"></param>
        /// <param name="expiry"></param>
        /// <returns></returns>
        Task<bool> KeyExpireAsync(string key, TimeSpan? expiry);

        /// <summary>
        /// Set the expiration time of the Key
        /// </summary>
        /// <param name="key"></param>
        /// <param name="expiry"></param>
        /// <returns></returns>
        Task<bool> KeyExpireAsync(RedisKeys key, TimeSpan? expiry);

        #endregion Key Async

        #region Lock Async

        /// <summary>
        /// Locks the take asynchronous.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <param name="expiry">The expiry. Default 10 minutes.</param>
        /// <returns></returns>
        Task<bool> LockTakeAsync(RedisLockKeys key, string value, TimeSpan? expiry = null);

        /// <summary>
        /// Locks the take asynchronous.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <param name="expiry">The expiry. Default 10 minutes.</param>
        /// <returns></returns>
        Task<bool> LockTakeAsync(string key, string value, TimeSpan? expiry = null);

        /// <summary>
        /// Locks the release asynchronous.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        Task<bool> LockReleaseAsync(RedisLockKeys key, string value);

        /// <summary>
        /// Locks the release asynchronous.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        Task<bool> LockReleaseAsync(string key, string value);

        #endregion Lock Async

        long HashDelete(RedisKeys key, IEnumerable<string> fields);

        bool HashDelete(RedisKeys key, string field);

        bool HashDelete(RedisKeys key, RedisFields field);

        long HashDelete(string key, IEnumerable<string> fields);

        bool HashDelete(string key, string field);

        bool HashExists(RedisKeys key, string field);

        bool HashExists(string key, string field);

        string HashGet(RedisKeys key, string field);

        string HashGet(RedisKeys key, RedisFields field);

        string HashGet(string key, string field);

        IEnumerable<T> HashGet<T>(RedisKeys key);

        T HashGet<T>(RedisKeys key, string field);

        T HashGet<T>(RedisKeys key, RedisFields field);

        IEnumerable<T> HashGet<T>(string key);

        T HashGet<T>(string key, string field);

        IEnumerable<string> HashScan(RedisKeys key, string pattern);

        IEnumerable<string> HashScan(string key, string pattern);

        IEnumerable<T> HashScan<T>(RedisKeys key, string pattern);

        IEnumerable<T> HashScan<T>(string key, string pattern);

        bool HashSet(RedisKeys key, string field, object value);

        bool HashSet(RedisKeys key, string field, string value);

        bool HashSet(RedisKeys key, RedisFields field, object value);

        bool HashSet(RedisKeys key, RedisFields field, string value);

        bool HashSet(string key, string field, object value);

        bool HashSet(string key, string field, string value);

        string ListLeftPop(RedisKeys key);

        string ListLeftPop(string key);

        T ListLeftPop<T>(RedisKeys key);

        T ListLeftPop<T>(string key);

        long ListLeftPush(RedisKeys key, string value);

        long ListLeftPush(string key, string value);

        long ListLeftPush<T>(RedisKeys key, T value);

        long ListLeftPush<T>(string key, T value);

        long ListLength(RedisKeys key);

        long ListLength(string key);

        long ListRemove(RedisKeys key, string value);

        long ListRemove(string key, string value);

        string ListRightPop(RedisKeys key);

        string ListRightPop(string key);

        T ListRightPop<T>(RedisKeys key);

        T ListRightPop<T>(string key);

        long ListRightPush(RedisKeys key, string value);

        long ListRightPush(string key, string value);

        long ListRightPush<T>(RedisKeys key, T value);

        long ListRightPush<T>(string key, T value);

        bool SortedSetAdd(RedisKeys key, string member, double score);

        bool SortedSetAdd(string key, string member, double score);

        bool SortedSetAdd<T>(RedisKeys key, T member, double score);

        bool SortedSetAdd<T>(string key, T member, double score);

        double SortedSetIncrement(RedisKeys key, string member, double value = 1);

        double SortedSetIncrement(string key, string member, double value = 1);

        long SortedSetLength(RedisKeys key);

        bool SortedSetLength(RedisKeys key, string memebr);

        long SortedSetLength(string key);

        bool SortedSetLength(string key, string memebr);

        IEnumerable<string> SortedSetRangeByRank(RedisKeys key, long start = 0, long stop = -1, Order order = Order.Ascending);

        IEnumerable<string> SortedSetRangeByRank(string key, long start = 0, long stop = -1, Order order = Order.Ascending);

        IEnumerable<string> SortedSetRangeByScore(RedisKeys key, double start = double.NegativeInfinity, double stop = double.PositiveInfinity);

        IEnumerable<string> SortedSetRangeByScore(string key, double start = double.NegativeInfinity, double stop = double.PositiveInfinity);

        IEnumerable<SortedSetEntry> SortedSetScan(RedisKeys key, string pattern);

        IEnumerable<SortedSetEntry> SortedSetScan(string key, string pattern);

        long SortedSetRemoveRangeByScore(RedisKeys key, double start, double stop);

        long SortedSetRemoveRangeByScore(string key, double start, double stop);

        long SortedSetRemove(RedisKeys key, params RedisValue[] member);

        long SortedSetRemove(string key, params RedisValue[] member);
    }
}