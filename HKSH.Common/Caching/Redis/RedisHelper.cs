using Microsoft.Extensions.DependencyInjection;

namespace HKSH.Common.Caching.Redis
{
    /// <summary>
    /// Redis Helper
    /// </summary>
    public class RedisHelper : IService
    {
        /// <summary>
        /// The service provider
        /// </summary>
        private readonly IServiceProvider _serviceProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="RedisHelper"/> class.
        /// </summary>
        /// <param name="serviceProvider">The service provider.</param>
        public RedisHelper(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        /// <summary>
        /// Checks the redis UUID exist.
        /// </summary>
        /// <param name="uuid">The UUID.</param>
        /// <returns></returns>
        public bool CheckRedisUuidExist(Guid uuid)
        {
            var redisRepository = _serviceProvider.GetService<IRedisRepository>();
            var redisUuid = redisRepository?.GetString(uuid.ToString());
            return string.IsNullOrEmpty(redisUuid);
        }

        /// <summary>
        /// Sets the UUID to redis.
        /// </summary>
        /// <param name="uuid">The UUID.</param>
        public void SetUuidToRedis(Guid uuid) 
        {
            var redisRepository = _serviceProvider.GetService<IRedisRepository>();
            redisRepository?.SetString(uuid.ToString(), uuid.ToString(), TimeSpan.FromHours(24));
        }
    }
}
