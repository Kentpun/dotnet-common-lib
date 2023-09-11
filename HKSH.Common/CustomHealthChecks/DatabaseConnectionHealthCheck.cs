using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace HKSH.Common.CustomHealthChecks
{
    /// <summary>
    /// Database Connection HealthCheck
    /// </summary>
    /// <typeparam name="TDbContext">The type of the database context.</typeparam>
    /// <seealso cref="Microsoft.Extensions.Diagnostics.HealthChecks.IHealthCheck" />
    public class DatabaseConnectionHealthCheck<TDbContext> : IHealthCheck where TDbContext : DbContext
    {
        /// <summary>
        /// The database context
        /// </summary>
        private readonly TDbContext _dbContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="DatabaseConnectionHealthCheck{TDbContext}"/> class.
        /// </summary>
        /// <param name="dbContext">The database context.</param>
        public DatabaseConnectionHealthCheck(TDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// Runs the health check, returning the status of the component being checked.
        /// </summary>
        /// <param name="context">A context object associated with the current execution.</param>
        /// <param name="cancellationToken">A <see cref="T:System.Threading.CancellationToken" /> that can be used to cancel the health check.</param>
        /// <returns>
        /// A <see cref="T:System.Threading.Tasks.Task`1" /> that completes when the health check has finished, yielding the status of the component being checked.
        /// </returns>
        /// <exception cref="System.NotImplementedException"></exception>
        Task<HealthCheckResult> IHealthCheck.CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken)
        {
            try
            {
                _dbContext.Database.OpenConnectionAsync(cancellationToken);
                _dbContext.Database.CloseConnection();
                return Task.FromResult(HealthCheckResult.Healthy());
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to connect to the database: {ex?.Message}", ex);
                return Task.FromResult(HealthCheckResult.Unhealthy("Failed to connect to the database.", ex));
            }

            throw new NotImplementedException();
        }
    }
}