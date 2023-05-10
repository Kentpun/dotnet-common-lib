using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace HKSH.Common.RabbitMQ
{
    /// <summary>
    /// MessageQueueHostedService
    /// </summary>
    /// <seealso cref="IHostedService" />
    public class MessageQueueHostedService : IHostedService
    {
        /// <summary>
        /// The service provider
        /// </summary>
        private readonly IServiceProvider _serviceProvider;

        /// <summary>
        /// The options
        /// </summary>
        private readonly RabbitMQOptions _options;

        /// <summary>
        /// Initializes a new instance of the <see cref="MessageQueueHostedService"/> class.
        /// </summary>
        /// <param name="serviceProvider">The service provider.</param>
        /// <param name="options">The options.</param>
        public MessageQueueHostedService(IServiceProvider serviceProvider, IOptions<RabbitMQOptions> options)
        {
            _serviceProvider = serviceProvider;
            _options = options.Value;
        }

        /// <summary>
        /// Triggered when the application host is ready to start the service.
        /// </summary>
        /// <param name="cancellationToken">Indicates that the start process has been aborted.</param>
        /// <returns></returns>
        public Task StartAsync(CancellationToken cancellationToken)
        {
            if (!_options.Enable) return Task.CompletedTask;
            var consumers = _serviceProvider.GetServices<IMessageQueueConsumer>();
            if (consumers.Any())
            {
                foreach (var consumer in consumers)
                {
                    consumer.Subscribe();
                }
            }
            return Task.CompletedTask;
        }

        /// <summary>
        /// Triggered when the application host is performing a graceful shutdown.
        /// </summary>
        /// <param name="cancellationToken">Indicates that the shutdown process should no longer be graceful.</param>
        /// <returns></returns>
        public Task StopAsync(CancellationToken cancellationToken)
        {
            if (!_options.Enable) return Task.CompletedTask;
            return Task.CompletedTask;
        }
    }
}