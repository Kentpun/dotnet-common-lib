using Microsoft.AspNetCore.Http;
using WebApiClient;

namespace HKSH.Common.ServiceInvoker
{
    /// <summary>
    /// IServiceInvoker
    /// </summary>
    public interface IServiceInvoker
    {
        /// <summary>
        /// Creates the invoker.
        /// </summary>
        /// <typeparam name="TInterface">The type of the interface.</typeparam>
        /// <returns></returns>
        TInterface CreateInvoker<TInterface>() where TInterface : class, IHttpApi;

        /// <summary>
        /// Creates the invoker.
        /// </summary>
        /// <typeparam name="TInterface">The type of the interface.</typeparam>
        /// <param name="header">The header.</param>
        /// <returns></returns>
        TInterface CreateInvoker<TInterface>(IHeaderDictionary header) where TInterface : class, IHttpApi;
    }
}