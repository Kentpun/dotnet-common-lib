using HKSH.Common.Repository.Database;
using Microsoft.Extensions.DependencyInjection;

namespace ABI.ArtWork.Common
{
    /// <summary>
    /// DIExtension
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