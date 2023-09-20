using Nest;

namespace HKSH.Common.Elastic
{
    /// <summary>
    /// IElastic search client provider
    /// </summary>
    public interface IElasticSearchClientProvider
    {
        /// <summary>
        /// Gets the client.
        /// </summary>
        /// <returns></returns>
        IElasticClient GetClient();
    }
}