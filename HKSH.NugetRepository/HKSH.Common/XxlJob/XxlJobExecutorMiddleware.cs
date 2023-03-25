using DotXxlJob.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace HKSH.Common.XxlJob
{
    /// <summary>
    /// XxlJobExecutorMiddleware
    /// </summary>
    public class XxlJobExecutorMiddleware
    {
        /// <summary>
        /// The provider
        /// </summary>
        private readonly IServiceProvider _provider;

        /// <summary>
        /// The next
        /// </summary>
        private readonly RequestDelegate _next;

        /// <summary>
        /// The RPC service
        /// </summary>
        private readonly XxlRestfulServiceHandler _rpcService;

        /// <summary>
        /// Initializes a new instance of the <see cref="XxlJobExecutorMiddleware"/> class.
        /// </summary>
        /// <param name="provider">The provider.</param>
        /// <param name="next">The next.</param>
        public XxlJobExecutorMiddleware(IServiceProvider provider, RequestDelegate next)
        {
            _provider = provider;
            _next = next;
            _rpcService = _provider.GetRequiredService<XxlRestfulServiceHandler>();
        }

        /// <summary>
        /// Invokes the specified context.
        /// </summary>
        /// <param name="context">The context.</param>
        public async Task Invoke(HttpContext context)
        {
            string? contentType = context.Request.ContentType;

            if ("POST".Equals(context.Request.Method, StringComparison.OrdinalIgnoreCase)
                && !string.IsNullOrEmpty(contentType)
                && contentType.ToLower().StartsWith("application/json"))
            {
                await _rpcService.HandlerAsync(context.Request, context.Response);
                return;
            }

            await _next.Invoke(context);
        }
    }
}