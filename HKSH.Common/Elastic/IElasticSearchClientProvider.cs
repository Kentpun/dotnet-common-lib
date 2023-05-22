using Nest;

namespace HKSH.Common.Elastic
{
    public interface IElasticSearchClientProvider
    {
        ElasticClient GetClient();
    }
}
