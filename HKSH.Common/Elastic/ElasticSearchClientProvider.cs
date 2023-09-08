using HKSH.Common.RabbitMQ;
using Microsoft.Extensions.Options;
using Nest;

namespace HKSH.Common.Elastic
{
    /// <summary>
    /// ElasticSearch
    /// </summary>
    public class ElasticSearchClientProvider : IElasticSearchClientProvider
    {
        /// <summary>
        /// The configuration
        /// </summary>
        private readonly IOptions<ElasticSearchOptions> _options;

        /// <summary>
        /// The client
        /// </summary>
        private ElasticClient? _client;

        /// <summary>
        /// Initializes a new instance of the <see cref="ElasticSearchClientProvider"/> class.
        /// </summary>
        /// <param name="options">The options.</param>
        public ElasticSearchClientProvider(IOptions<ElasticSearchOptions> options)
        {
            _options = options;
        }

        /// <summary>
        /// Gets the client.
        /// </summary>
        /// <returns></returns>
        public ElasticClient GetClient()
        {
            if (_client != null)
            {
                return _client;
            }

            InitClient();

            return _client ?? new ElasticClient();
        }

        /// <summary>
        /// Initializes the client.
        /// </summary>
        private void InitClient()
        {
            Uri node = new Uri(_options.Value.Url ?? string.Empty);

            _client = new ElasticClient(new ConnectionSettings(node).DefaultIndex(_options.Value.DefaultIndex ?? "default").CertificateFingerprint(_options.Value.CertificateFingerprint).BasicAuthentication(_options.Value.UserName, _options.Value.Password));
        }
    }
}