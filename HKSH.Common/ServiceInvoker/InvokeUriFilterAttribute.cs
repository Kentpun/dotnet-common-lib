﻿using WebApiClient.Attributes;
using WebApiClient.Contexts;

namespace HKSH.Common.ServiceInvoker
{
    /// <summary>
    /// InvokeUriFilterAttribute
    /// </summary>
    /// <seealso cref="ApiActionFilterAttribute" />
    public class InvokeUriFilterAttribute : ApiActionFilterAttribute
    {
        /// <summary>
        /// The version
        /// </summary>
        private readonly string _version = string.Empty;

        /// <summary>
        /// Initializes a new instance of the <see cref="InvokeUriFilterAttribute"/> class.
        /// </summary>
        /// <param name="Version">The version.</param>
        public InvokeUriFilterAttribute(string Version)
        {
            _version = Version;
        }

        /// <summary>
        /// 准备请求之前
        /// </summary>
        /// <param name="context">上下文</param>
        /// <returns></returns>
        public override Task OnBeginRequestAsync(ApiActionContext context)
        {
            if (context.RequestMessage.RequestUri != null)
            {
                context.RequestMessage.RequestUri = new Uri(context.RequestMessage.RequestUri.OriginalString.Replace(context.RequestMessage.RequestUri.Host, $"{context.RequestMessage.RequestUri?.Host}/{_version}" ?? string.Empty));
            }
            return base.OnBeginRequestAsync(context);
        }
    }
}