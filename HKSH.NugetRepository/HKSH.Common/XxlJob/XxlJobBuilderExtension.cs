using Microsoft.AspNetCore.Builder;

namespace HKSH.Common.XxlJob
{
    /// <summary>
    /// XxlJobBuilderExtension
    /// </summary>
    public static class XxlJobBuilderExtension
    {
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