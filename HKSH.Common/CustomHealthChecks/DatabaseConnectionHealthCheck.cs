using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace HKSH.Common.CustomHealthChecks
{
	public class DatabaseConnectionHealthCheck<TDbContext> : IHealthCheck where TDbContext : DbContext
    {
        private readonly TDbContext _dbContext;

        public DatabaseConnectionHealthCheck(TDbContext dbContext)
		{
            _dbContext = dbContext;
		}

        Task<HealthCheckResult> IHealthCheck.CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            try
            {
                _dbContext.Database.OpenConnectionAsync(cancellationToken);
                _dbContext.Database.CloseConnection();
                return Task.FromResult(HealthCheckResult.Healthy());
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed to connect to the database.", ex);
                return Task.FromResult(HealthCheckResult.Unhealthy("Failed to connect to the database.", ex));
            }

            throw new NotImplementedException();
        }
    }
}

