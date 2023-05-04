﻿using Microsoft.AspNetCore.Http;
using WebApiClient;

namespace HKSH.Common.ServiceInvoker
{
    /// <summary>
    /// ServiceInvoker
    /// </summary>
    /// <seealso cref="IServiceInvoker" />
    public class ServiceInvoker : IServiceInvoker
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceInvoker"/> class.
        /// </summary>
        /// <param name="httpContextAccessor">The HTTP context accessor.</param>
        public ServiceInvoker(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// Creates the invoker.
        /// </summary>
        /// <typeparam name="TInterface">The type of the interface.</typeparam>
        /// <param name="url">The URL.</param>
        /// <returns></returns>
        public TInterface CreateInvoker<TInterface>(string? hostPrefix = "") where TInterface : class, IHttpApi
        {
            var factory = new HttpApiFactory(typeof(TInterface));
            factory.ConfigureHttpApiConfig(options =>
            {
                //重新拼接HttpHost
                options.HttpHost = string.IsNullOrEmpty(hostPrefix) ? options.HttpHost : new Uri(hostPrefix);

                //Add token
                var headers = _httpContextAccessor.HttpContext?.Request.Headers;

                if (headers != null)
                {
                    var authentication = headers[HttpRequestHeader.Authorization.ToString()];
                    if (!string.IsNullOrEmpty(authentication))
                    {
                        options.HttpClient.DefaultRequestHeaders.Add(HttpRequestHeader.Authorization.ToString(),
                            authentication.ToString());
                    }
                }
            });

            return factory.CreateHttpApi() as TInterface;
        }

        /// <summary>
        /// Creates the invoker.
        /// </summary>
        /// <typeparam name="TInterface">The type of the interface.</typeparam>
        /// <param name="header">The header.</param>
        /// <param name="url">The URL.</param>
        /// <returns></returns>
        public TInterface CreateInvoker<TInterface>(IHeaderDictionary header, string? hostPrefix = "") where TInterface : class, IHttpApi
        {
            var factory = new HttpApiFactory(typeof(TInterface));
            factory.ConfigureHttpApiConfig(options =>
            {
                //重新拼接HttpHost
                options.HttpHost = string.IsNullOrEmpty(hostPrefix) ? options.HttpHost : new Uri(hostPrefix);

                //Add token
                var headers = _httpContextAccessor.HttpContext?.Request.Headers;

                var authentication = headers?[HttpRequestHeader.Authorization.ToString()];
                if (!string.IsNullOrEmpty(authentication))
                {
                    options.HttpClient.DefaultRequestHeaders.Add(HttpRequestHeader.Authorization.ToString(),
                        authentication.ToString());
                }

                if (header?.Count > 0)
                {
                    foreach (var h in header)
                    {
                        options.HttpClient.DefaultRequestHeaders.Add(h.Key, Convert.ToString(h.Value));
                    }
                }
            });

            return factory.CreateHttpApi() as TInterface;
        }
    }
}