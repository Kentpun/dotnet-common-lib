using HKSH.Common.Repository.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace HKSH.Common.Extensions
{
    /// <summary>
    /// Repository extension
    /// </summary>
    public static class RepositoryExtension
    {
        /// <summary>
        /// Add db and inject repository and unitofwork
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddDatabase<TDbContext>(this IServiceCollection services) where TDbContext : DbContext
        {
            services.AddTransient<IUnitOfWork, UnitOfWork<TDbContext>>();
            services.AddTransient(typeof(IRepository<>), typeof(Repository<>));

            return services;
        }
    }
}