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
            context.RequestMessage.RequestUri = new Uri($"{context.RequestMessage.RequestUri?.Scheme}://{context.RequestMessage.RequestUri?.Host}/{_version}{context.RequestMessage.RequestUri?.LocalPath}");
            return base.OnBeginRequestAsync(context);
        }
    }
}
