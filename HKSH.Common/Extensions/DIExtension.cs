using HKSH.Common.Context;
using HKSH.Common.Repository.Database;
using Microsoft.Extensions.DependencyInjection;

namespace HKSH.Common.Extensions
{
    /// <summary>
    /// DI Extension
    /// </summary>
    public static class DIExtension
    {
        /// <summary>
        /// Adds the current context.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <returns></returns>
        public static IServiceCollection AddCurrentContext(this IServiceCollection services)
        {
            services.AddScoped<ICurrentContext, CurrentContext>();
            services.AddScoped<IRepositoryCurrentContext, CurrentContext>();
            return services;
        }
    }
}