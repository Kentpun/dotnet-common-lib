using HKSH.Common.Caching.Redis;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace HKSH.Common.CustomHealthChecks
{
    /// <summary>
    /// Redis Connection HealthCheck
    /// </summary>
    /// <seealso cref="IHealthCheck" />
    public class RedisConnectionHealthCheck : IHealthCheck
    {
        /// <summary>
        /// The redis repository
        /// </summary>
        private readonly IRedisRepository _redisRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="RedisConnectionHealthCheck"/> class.
        /// </summary>
        /// <param name="redisRepository">The redis repository.</param>
        public RedisConnectionHealthCheck(IRedisRepository redisRepository)
        {
            _redisRepository = redisRepository;
        }

        /// <summary>
        /// Runs the health check, returning the status of the component being checked.
        /// </summary>
        /// <param name="context">A context object associated with the current execution.</param>
        /// <param name="cancellationToken">A <see cref="T:System.Threading.CancellationToken" /> that can be used to cancel the health check.</param>
        /// <returns>
        /// A <see cref="T:System.Threading.Tasks.Task`1" /> that completes when the health check has finished, yielding the status of the component being checked.
        /// </returns>
        Task<HealthCheckResult> IHealthCheck.CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken)
        {
            try
            {
                // Perform a test operation on the Redis repository
                // For example, you can check if the Redis connection is available or execute a simple Redis command
                // Replace this with the appropriate method from your Redis repository
                bool isRedisConnectionHealthy = _redisRepository.Database != null;

                if (isRedisConnectionHealthy)
                {
                    return Task.FromResult(HealthCheckResult.Healthy());
                }
                else
                {
                    return Task.FromResult(HealthCheckResult.Unhealthy("Redis connection is not healthy."));
                }
            }
            catch (Exception ex)
            {
                return Task.FromResult(HealthCheckResult.Unhealthy("Error occurred while checking Redis connection.", ex));
            }
        }
    }
}