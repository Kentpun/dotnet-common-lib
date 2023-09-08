using System;
using HKSH.Common.Caching.Redis;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace HKSH.Common.CustomHealthChecks
{
	public class RedisConnectionHealthCheck : IHealthCheck
    {
        private readonly IRedisRepository _redisRepository;

        public RedisConnectionHealthCheck(IRedisRepository redisRepository)
		{
            _redisRepository = redisRepository;
        }

        Task<HealthCheckResult> IHealthCheck.CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken)
        {
            try
            {
                // Perform a test operation on the Redis repository
                // For example, you can check if the Redis connection is available or execute a simple Redis command
                // Replace this with the appropriate method from your Redis repository
                //bool isRedisConnectionHealthy = _redisRepository.Database();


                //if (isRedisConnectionHealthy)
                //{
                //    return Task.FromResult(HealthCheckResult.Healthy());
                //}
                //else
                //{
                //    return Task.FromResult(HealthCheckResult.Unhealthy("Redis connection is not healthy."));
                //}
            }
            catch (Exception ex)
            {
                return Task.FromResult(HealthCheckResult.Unhealthy("Error occurred while checking Redis connection.", ex));
            }
        }
    }
}

