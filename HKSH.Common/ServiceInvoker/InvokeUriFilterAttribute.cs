using Microsoft.Extensions.DependencyInjection;
using WebApiClient;
using WebApiClient.Attributes;
using WebApiClient.Contexts;
namespace HKSH.Common.ServiceInvoker
{
    /// <summary>
    /// InvokeUriFilterAttribute
    /// </summary>
    /// <seealso cref="WebApiClient.Attributes.ApiActionFilterAttribute" />
    public class InvokeUriFilterAttribute : ApiActionFilterAttribute
    {
        private string _version = string.Empty;

        public InvokeUriFilterAttribute(string Version)
        {
            _version = Version;
        }

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
