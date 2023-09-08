using HKSH.Common.Enums.Redis;
using StackExchange.Redis;

namespace HKSH.Common.Caching.Redis
{
    /// <summary>
    /// IRedis Repository
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

        /// <summary>
        /// Hashes the delete.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="fields">The fields.</param>
        /// <returns></returns>
        long HashDelete(RedisKeys key, IEnumerable<string> fields);

        /// <summary>
        /// Hashes the delete.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="field">The field.</param>
        /// <returns></returns>
        bool HashDelete(RedisKeys key, string field);

        /// <summary>
        /// Hashes the delete.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="field">The field.</param>
        /// <returns></returns>
        bool HashDelete(RedisKeys key, RedisFields field);

        /// <summary>
        /// Hashes the delete.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="fields">The fields.</param>
        /// <returns></returns>
        long HashDelete(string key, IEnumerable<string> fields);

        /// <summary>
        /// Hashes the delete.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="field">The field.</param>
        /// <returns></returns>
        bool HashDelete(string key, string field);

        /// <summary>
        /// Hashes the exists.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="field">The field.</param>
        /// <returns></returns>
        bool HashExists(RedisKeys key, string field);

        /// <summary>
        /// Hashes the exists.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="field">The field.</param>
        /// <returns></returns>
        bool HashExists(string key, string field);

        /// <summary>
        /// Hashes the get.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="field">The field.</param>
        /// <returns></returns>
        string HashGet(RedisKeys key, string field);

        /// <summary>
        /// Hashes the get.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="field">The field.</param>
        /// <returns></returns>
        string HashGet(RedisKeys key, RedisFields field);

        /// <summary>
        /// Hashes the get.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="field">The field.</param>
        /// <returns></returns>
        string HashGet(string key, string field);

        /// <summary>
        /// Hashes the get.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        IEnumerable<T> HashGet<T>(RedisKeys key);

        /// <summary>
        /// Hashes the get.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">The key.</param>
        /// <param name="field">The field.</param>
        /// <returns></returns>
        T HashGet<T>(RedisKeys key, string field);

        /// <summary>
        /// Hashes the get.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">The key.</param>
        /// <param name="field">The field.</param>
        /// <returns></returns>
        T HashGet<T>(RedisKeys key, RedisFields field);

        /// <summary>
        /// Hashes the get.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        IEnumerable<T> HashGet<T>(string key);

        /// <summary>
        /// Hashes the get.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">The key.</param>
        /// <param name="field">The field.</param>
        /// <returns></returns>
        T HashGet<T>(string key, string field);

        /// <summary>
        /// Hashes the scan.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="pattern">The pattern.</param>
        /// <returns></returns>
        IEnumerable<string> HashScan(RedisKeys key, string pattern);

        /// <summary>
        /// Hashes the scan.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="pattern">The pattern.</param>
        /// <returns></returns>
        IEnumerable<string> HashScan(string key, string pattern);

        /// <summary>
        /// Hashes the scan.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">The key.</param>
        /// <param name="pattern">The pattern.</param>
        /// <returns></returns>
        IEnumerable<T> HashScan<T>(RedisKeys key, string pattern);

        /// <summary>
        /// Hashes the scan.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">The key.</param>
        /// <param name="pattern">The pattern.</param>
        /// <returns></returns>
        IEnumerable<T> HashScan<T>(string key, string pattern);

        /// <summary>
        /// Hashes the set.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="field">The field.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        bool HashSet(RedisKeys key, string field, object value);

        /// <summary>
        /// Hashes the set.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="field">The field.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        bool HashSet(RedisKeys key, string field, string value);

        /// <summary>
        /// Hashes the set.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="field">The field.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        bool HashSet(RedisKeys key, RedisFields field, object value);

        /// <summary>
        /// Hashes the set.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="field">The field.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        bool HashSet(RedisKeys key, RedisFields field, string value);

        /// <summary>
        /// Hashes the set.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="field">The field.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        bool HashSet(string key, string field, object value);

        /// <summary>
        /// Hashes the set.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="field">The field.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        bool HashSet(string key, string field, string value);

        /// <summary>
        /// Lists the left pop.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        string ListLeftPop(RedisKeys key);

        /// <summary>
        /// Lists the left pop.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        string ListLeftPop(string key);

        /// <summary>
        /// Lists the left pop.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        T ListLeftPop<T>(RedisKeys key);

        /// <summary>
        /// Lists the left pop.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        T ListLeftPop<T>(string key);

        /// <summary>
        /// Lists the left push.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        long ListLeftPush(RedisKeys key, string value);

        /// <summary>
        /// Lists the left push.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        long ListLeftPush(string key, string value);

        /// <summary>
        /// Lists the left push.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        long ListLeftPush<T>(RedisKeys key, T value);

        /// <summary>
        /// Lists the left push.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        long ListLeftPush<T>(string key, T value);

        /// <summary>
        /// Lists the length.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        long ListLength(RedisKeys key);

        /// <summary>
        /// Lists the length.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        long ListLength(string key);

        /// <summary>
        /// Lists the remove.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        long ListRemove(RedisKeys key, string value);

        /// <summary>
        /// Lists the remove.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        long ListRemove(string key, string value);

        /// <summary>
        /// Lists the right pop.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        string ListRightPop(RedisKeys key);

        /// <summary>
        /// Lists the right pop.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        string ListRightPop(string key);

        /// <summary>
        /// Lists the right pop.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        T ListRightPop<T>(RedisKeys key);

        /// <summary>
        /// Lists the right pop.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        T ListRightPop<T>(string key);

        /// <summary>
        /// Lists the right push.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        long ListRightPush(RedisKeys key, string value);

        /// <summary>
        /// Lists the right push.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        long ListRightPush(string key, string value);

        /// <summary>
        /// Lists the right push.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        long ListRightPush<T>(RedisKeys key, T value);

        /// <summary>
        /// Lists the right push.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        long ListRightPush<T>(string key, T value);

        /// <summary>
        /// Sorteds the set add.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="member">The member.</param>
        /// <param name="score">The score.</param>
        /// <returns></returns>
        bool SortedSetAdd(RedisKeys key, string member, double score);

        /// <summary>
        /// Sorteds the set add.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="member">The member.</param>
        /// <param name="score">The score.</param>
        /// <returns></returns>
        bool SortedSetAdd(string key, string member, double score);

        /// <summary>
        /// Sorteds the set add.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">The key.</param>
        /// <param name="member">The member.</param>
        /// <param name="score">The score.</param>
        /// <returns></returns>
        bool SortedSetAdd<T>(RedisKeys key, T member, double score);

        /// <summary>
        /// Sorteds the set add.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">The key.</param>
        /// <param name="member">The member.</param>
        /// <param name="score">The score.</param>
        /// <returns></returns>
        bool SortedSetAdd<T>(string key, T member, double score);

        /// <summary>
        /// Sorteds the set increment.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="member">The member.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        double SortedSetIncrement(RedisKeys key, string member, double value = 1);

        /// <summary>
        /// Sorteds the set increment.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="member">The member.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        double SortedSetIncrement(string key, string member, double value = 1);

        /// <summary>
        /// Sorteds the length of the set.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        long SortedSetLength(RedisKeys key);

        /// <summary>
        /// Sorteds the length of the set.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="memebr">The memebr.</param>
        /// <returns></returns>
        bool SortedSetLength(RedisKeys key, string memebr);

        /// <summary>
        /// Sorteds the length of the set.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        long SortedSetLength(string key);

        /// <summary>
        /// Sorteds the length of the set.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="memebr">The memebr.</param>
        /// <returns></returns>
        bool SortedSetLength(string key, string memebr);

        /// <summary>
        /// Sorteds the set range by rank.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="start">The start.</param>
        /// <param name="stop">The stop.</param>
        /// <param name="order">The order.</param>
        /// <returns></returns>
        IEnumerable<string> SortedSetRangeByRank(RedisKeys key, long start = 0, long stop = -1, Order order = Order.Ascending);

        /// <summary>
        /// Sorteds the set range by rank.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="start">The start.</param>
        /// <param name="stop">The stop.</param>
        /// <param name="order">The order.</param>
        /// <returns></returns>
        IEnumerable<string> SortedSetRangeByRank(string key, long start = 0, long stop = -1, Order order = Order.Ascending);

        /// <summary>
        /// Sorteds the set range by score.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="start">The start.</param>
        /// <param name="stop">The stop.</param>
        /// <returns></returns>
        IEnumerable<string> SortedSetRangeByScore(RedisKeys key, double start = double.NegativeInfinity, double stop = double.PositiveInfinity);

        /// <summary>
        /// Sorteds the set range by score.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="start">The start.</param>
        /// <param name="stop">The stop.</param>
        /// <returns></returns>
        IEnumerable<string> SortedSetRangeByScore(string key, double start = double.NegativeInfinity, double stop = double.PositiveInfinity);

        /// <summary>
        /// Sorteds the set scan.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="pattern">The pattern.</param>
        /// <returns></returns>
        IEnumerable<SortedSetEntry> SortedSetScan(RedisKeys key, string pattern);

        /// <summary>
        /// Sorteds the set scan.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="pattern">The pattern.</param>
        /// <returns></returns>
        IEnumerable<SortedSetEntry> SortedSetScan(string key, string pattern);

        /// <summary>
        /// Sorteds the set remove range by score.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="start">The start.</param>
        /// <param name="stop">The stop.</param>
        /// <returns></returns>
        long SortedSetRemoveRangeByScore(RedisKeys key, double start, double stop);

        /// <summary>
        /// Sorteds the set remove range by score.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="start">The start.</param>
        /// <param name="stop">The stop.</param>
        /// <returns></returns>
        long SortedSetRemoveRangeByScore(string key, double start, double stop);

        /// <summary>
        /// Sorteds the set remove.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="member">The member.</param>
        /// <returns></returns>
        long SortedSetRemove(RedisKeys key, params RedisValue[] member);

        /// <summary>
        /// Sorteds the set remove.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="member">The member.</param>
        /// <returns></returns>
        long SortedSetRemove(string key, params RedisValue[] member);

        /// <summary>
        /// Hashes the fields.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        IEnumerable<string> HashFields(string key);
    }
}