using HKSH.Common.Constants;
using HKSH.Common.Context;
using Microsoft.AspNetCore.Http;
using WebApiClient;

namespace HKSH.Common.ServiceInvoker
{
    /// <summary>
    /// ServiceInvoker
    /// </summary>
    /// <seealso cref="IServiceInvoker" />
    public class ServiceInvoker : IServiceInvoker
    {
        /// <summary>
        /// The HTTP context accessor
        /// </summary>
        private readonly IHttpContextAccessor _httpContextAccessor;

        /// <summary>
        /// The current context
        /// </summary>
        private readonly ICurrentContext _currentContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceInvoker"/> class.
        /// </summary>
        /// <param name="httpContextAccessor">The HTTP context accessor.</param>
        /// <param name="currentContext">The current context.</param>
        public ServiceInvoker(IHttpContextAccessor httpContextAccessor, ICurrentContext currentContext)
        {
            _httpContextAccessor = httpContextAccessor;
            _currentContext = currentContext;
        }

        /// <summary>
        /// Creates the invoker.
        /// </summary>
        /// <typeparam name="TInterface">The type of the interface.</typeparam>
        /// <param name="hostPrefix"></param>
        /// <returns></returns>
        public TInterface CreateInvoker<TInterface>(string? hostPrefix = "") where TInterface : class, IHttpApi
        {
            HttpApiFactory factory = new HttpApiFactory(typeof(TInterface));
            factory.ConfigureHttpApiConfig(options =>
            {
                //重新拼接HttpHost
                options.HttpHost = string.IsNullOrEmpty(hostPrefix) ? options.HttpHost : new Uri($"{hostPrefix}", UriKind.RelativeOrAbsolute);
                options.GlobalFilters.Add(new InvokeUriFilterAttribute($"v{ApiVersionConstant.VERSION_ONE}.{ApiVersionConstant.VERSION_ZERO}"));

                //Add token
                IHeaderDictionary? headers = _httpContextAccessor.HttpContext?.Request.Headers;

                if (headers != null)
                {
                    Microsoft.Extensions.Primitives.StringValues authentication = headers[GlobalConstant.AUTH_HEADER];
                    if (!string.IsNullOrEmpty(authentication))
                    {
                        options.HttpClient.DefaultRequestHeaders.Add(GlobalConstant.AUTH_HEADER,
                            authentication.ToString());
                    }
                }

                string reallyUserId = _currentContext.CurrentUserId;
                if (!string.IsNullOrEmpty(reallyUserId))
                {
                    options.HttpClient.DefaultRequestHeaders.Add(GlobalConstant.CURRENT_USER_CODE, reallyUserId);
                }
            });

            return (factory.CreateHttpApi() as TInterface)!;
        }

        /// <summary>
        /// Creates the invoker.
        /// </summary>
        /// <typeparam name="TInterface">The type of the interface.</typeparam>
        /// <param name="header">The header.</param>
        /// <param name="hostPrefix"></param>
        /// <returns></returns>
        public TInterface CreateInvoker<TInterface>(IHeaderDictionary header, string? hostPrefix = "") where TInterface : class, IHttpApi
        {
            HttpApiFactory factory = new HttpApiFactory(typeof(TInterface));
            factory.ConfigureHttpApiConfig(options =>
            {
                //重新拼接HttpHost
                options.HttpHost = string.IsNullOrEmpty(hostPrefix) ? options.HttpHost : new Uri($"{hostPrefix}", UriKind.RelativeOrAbsolute);
                options.GlobalFilters.Add(new InvokeUriFilterAttribute($"v{ApiVersionConstant.VERSION_ONE}.{ApiVersionConstant.VERSION_ZERO}"));

                //Add token
                IHeaderDictionary? headers = _httpContextAccessor.HttpContext?.Request.Headers;

                Microsoft.Extensions.Primitives.StringValues? authentication = headers?[GlobalConstant.AUTH_HEADER];
                if (!string.IsNullOrEmpty(authentication))
                {
                    options.HttpClient.DefaultRequestHeaders.Add(GlobalConstant.AUTH_HEADER,
                        authentication.ToString());
                }

                if (header?.Count > 0)
                {
                    foreach (KeyValuePair<string, Microsoft.Extensions.Primitives.StringValues> h in header)
                    {
                        options.HttpClient.DefaultRequestHeaders.Add(h.Key, Convert.ToString(h.Value));
                    }
                }

                string reallyUserId = _currentContext.CurrentUserId;
                if (!string.IsNullOrEmpty(reallyUserId))
                {
                    options.HttpClient.DefaultRequestHeaders.Add(GlobalConstant.CURRENT_USER_CODE, reallyUserId);
                }
            });

            return (factory.CreateHttpApi() as TInterface)!;
        }
    }
}