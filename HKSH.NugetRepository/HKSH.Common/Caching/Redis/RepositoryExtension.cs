using Microsoft.Extensions.DependencyInjection;

namespace HKSH.Common.Caching.Redis
{
    /// <summary>
    /// Repository extension
    /// </summary>
    public static class RepositoryExtension
    {
        /// <summary>
        /// Adds the redis.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <param name="redisOptions">The redis options.</param>
        /// <returns></returns>
        public static IServiceCollection AddRedis(this IServiceCollection services, Action<RedisOptions> redisOptions)
        {
            services.Configure(redisOptions);
            services.AddScoped<IRedisRepository, RedisRepository>();
            return services;
        }
    }
}