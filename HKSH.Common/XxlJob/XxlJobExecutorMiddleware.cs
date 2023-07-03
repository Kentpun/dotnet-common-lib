using DotXxlJob.Core;
using HKSH.Common.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Nest;

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
        /// logger
        /// </summary>
        private readonly ILogger<XxlJobExecutorMiddleware> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="XxlJobExecutorMiddleware"/> class.
        /// </summary>
        /// <param name="provider">The provider.</param>
        /// <param name="next">The next.</param>
        public XxlJobExecutorMiddleware(IServiceProvider provider, RequestDelegate next, ILogger<XxlJobExecutorMiddleware> logger)
        {
            _provider = provider;
            _next = next;
            _rpcService = _provider.GetRequiredService<XxlRestfulServiceHandler>();
            _logger = logger;
        }

        /// <summary>
        /// Invokes the specified context.
        /// </summary>
        /// <param name="context">The context.</param>
        public async Task Invoke(HttpContext context)
        {
            
            _logger.LogInfo("=======" + context.Request.Host.Host);
            
            if (context.Request.Headers.Any(s => s.Key == "Authorization"))
            {
                _logger.LogInfo("进入 ==== app url");
                
                await _next.Invoke(context);
            }
            else
            {
                var contentType = context.Request.ContentType;

                if ("POST".Equals(context.Request.Method, StringComparison.OrdinalIgnoreCase) && !string.IsNullOrEmpty(contentType) && contentType.ToLower().StartsWith("application/json"))
                {
                    _logger.LogInfo("进入 ==== xxl job url");
                    await _rpcService.HandlerAsync(context.Request, context.Response);
                }
            }
        }
    }
}