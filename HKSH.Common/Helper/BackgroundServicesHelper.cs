using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace HKSH.Common.Helper
{
    /// <summary>
    /// BackgroundServicesHelper
    /// </summary>
    public static class BackgroundServicesHelper
    {
        /// <summary>
        /// Gets all child class.
        /// </summary>
        /// <param name="baseType">Type of the base.</param>
        /// <returns></returns>
        private static Type[] GetAllChildClass(Type baseType)
        {
            var types = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(a => a.GetTypes().Where(t => t.BaseType == baseType))
            .ToArray();

            return types;
        }

        /// <summary>
        /// Gets all background service.
        /// </summary>
        /// <returns></returns>
        public static Type[] GetAllBackgroundService()
        {
            return GetAllChildClass(typeof(BackgroundService));
        }

        /// <summary>
        /// Adds the background services.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <returns></returns>
        public static IServiceCollection AddBackgroundServices(this IServiceCollection services)
        {
            var backtypes = GetAllBackgroundService();
            foreach (var backtype in backtypes)
            {
                services.AddTransient(typeof(IHostedService), backtype);
            }
            return services;
        }
    }
}