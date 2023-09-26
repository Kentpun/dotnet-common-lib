using HKSH.Common.Extensions;
using HKSH.Common.ShareModel;
using HKSH.Common.ShareModel.Request;
using HKSH.Common.ShareModel.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace HKSH.Common.Filters
{
    /// <summary>
    /// ApiActionFilter
    /// </summary>
    /// <seealso cref="ActionFilterAttribute" />
    public class ApiActionFilterAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// The logger
        /// </summary>
        private readonly ILogger<ApiActionFilterAttribute> _logger;

        /// <summary>
        /// The API request
        /// </summary>
        private ApiRequest apiRequest = new();

        /// <summary>
        /// Initializes a new instance of the <see cref="ApiActionFilterAttribute"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        public ApiActionFilterAttribute(ILogger<ApiActionFilterAttribute> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// </summary>
        /// <param name="context"></param>
        /// <inheritdoc />
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            try
            {
                apiRequest = new ApiRequest
                {
                    RequestTime = DateTime.Now,
                    Method = context.HttpContext.Request.Method,
                    RequestIP = context.HttpContext.Connection?.RemoteIpAddress?.ToString(),
                    Url = $"{context.HttpContext.Request.Host}{context.HttpContext.Request.Path}"
                };

                if (apiRequest.Method == "GET")
                {
                    apiRequest.Parameters = context.HttpContext.Request.QueryString.Value;
                }
                else
                {
                    HttpContext httpContext = context.HttpContext;
                    HttpRequest request = httpContext.Request;
                    request.Body.Position = 0;
                    var sr = new StreamReader(request.Body);
                    request.Body.Position = 0;
                    string body = sr.ReadToEndAsync().Result;
                    apiRequest.Parameters = body;
                }
            }
            catch (Exception ex)
            {
                _logger.LogExc(ex, $"ApiActionFilterAttribute OnActionExecuting Exception:{ex.Message} Body:{JsonConvert.SerializeObject(apiRequest)}");
            }
            finally
            {
                base.OnActionExecuting(context);
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="context"></param>
        /// <inheritdoc />
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            try
            {
                string? response = context.Result?.ToJson();
                string? responseData = string.Empty;
                try
                {
                    ActionResponse<MessageResult>? messageResult = response?.DeserialeJson<ActionResponse<MessageResult>>();
                    apiRequest.IsSuccess = messageResult?.Value?.Success ?? messageResult?.StatusCode == 200;
                    responseData = messageResult?.Value?.ToJson();
                }
                catch
                {
                    ActionResponse<string>? messageResult = response?.DeserialeJson<ActionResponse<string>>();
                    apiRequest.IsSuccess = messageResult?.StatusCode == 200;
                    responseData = messageResult?.Value;
                }

                apiRequest.Response = responseData;
                apiRequest.ResponseTime = DateTime.Now;
            }
            catch (Exception ex)
            {
                _logger.LogExc(ex, $"ApiActionFilterAttribute OnActionExecuted Exception:{ex.Message} Body:{JsonConvert.SerializeObject(apiRequest)}");
            }
            finally
            {
                apiRequest.ExecutionTime = (apiRequest.ResponseTime - apiRequest.RequestTime).GetValueOrDefault().TotalMilliseconds;
                _logger.LogInfo(JsonConvert.SerializeObject(apiRequest));
                base.OnActionExecuted(context);
            }
        }
    }
}