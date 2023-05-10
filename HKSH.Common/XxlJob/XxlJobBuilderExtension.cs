using DotXxlJob.Core;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace HKSH.Common.XxlJob
{
    /// <summary>
    /// XxlJobBuilderExtension
    /// </summary>
    public static class XxlJobBuilderExtension
    {
        /// <summary>
        /// Addxxls the jobs.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <param name="configuration">The configuration.</param>
        /// <returns></returns>
        public static IServiceCollection AddxxlJobs(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddXxlJobExecutor(configuration);
            var types = Assembly.GetEntryAssembly()?.DefinedTypes.Where(a => typeof(IJobHandler).IsAssignableFrom(a));
            var methods = typeof(ServiceCollectionServiceExtensions).GetMethods();
            MethodInfo? method = methods.FirstOrDefault(m => m.Name == "AddSingleton" && m.IsGenericMethod && m.GetGenericArguments().Length == 2);
            if (types != null)
            {
                foreach (var type in types)
                {
                    var registerMethod = method?.MakeGenericMethod(new Type[] { typeof(IJobHandler), type });
                    registerMethod?.Invoke(services, new object[] { services });
                }
                services.AddAutoRegistry();
            }
            return services;
        }

        /// <summary>
        /// Uses the XXL job executor.
        /// </summary>
        /// <param name="this">The this.</param>
        /// <returns></returns>
        public static IApplicationBuilder UseXxlJobExecutor(this IApplicationBuilder @this)
        {
            return @this.UseMiddleware<XxlJobExecutorMiddleware>();
        }
    }
}