using HKSH.Common.Exceptions;
using HKSH.Common.Extensions;
using HKSH.Common.ShareModel;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net;

namespace HKSH.Common.Middlewares
{
    /// <summary>
    /// ExceptionHandleMiddleware
    /// </summary>
    public class ExceptionHandleMiddleware
    {
        /// <summary>
        /// The next
        /// </summary>
        private readonly RequestDelegate _next;

        /// <summary>
        /// The logger
        /// </summary>
        private readonly ILogger<ExceptionHandleMiddleware> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomErrorMiddleware"/> class.
        /// </summary>
        /// <param name="next">The next.</param>
        /// <param name="logger"></param>
        public ExceptionHandleMiddleware(RequestDelegate next, ILogger<ExceptionHandleMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        /// <summary>
        /// Invokes the specified HTTP context.
        /// </summary>
        /// <param name="httpContext">The HTTP context.</param>
        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        /// <summary>
        /// Handles the exception asynchronous.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="ex">The ex.</param>
        /// <returns></returns>
        private Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            HttpStatusCode code;
            if (ex is UnAuthorizedException)
            {
                code = HttpStatusCode.Unauthorized;
            }
            else
            {
                _logger.LogExc(ex);
                code = HttpStatusCode.OK;
            }

            string result = JsonConvert.SerializeObject(MessageResult.FailureResult(ex.Message));
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;
            return context.Response.WriteAsync(result);
        }
    }
}