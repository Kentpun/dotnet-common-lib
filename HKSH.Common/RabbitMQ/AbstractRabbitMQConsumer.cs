using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace HKSH.Common.RabbitMQ
{
    /// <summary>
    /// AbstractRabbitMQConsumer
    /// </summary>
    /// <seealso cref="HKSH.Common.RabbitMQ.IMessageQueueConsumer" />
    /// <seealso cref="IMessageQueueConsumer" />
    public abstract class AbstractRabbitMQConsumer : IMessageQueueConsumer
    {
        /// <summary>
        /// The service provider
        /// </summary>
        protected IServiceProvider _serviceProvider;

        /// <summary>
        /// The logger
        /// </summary>
        protected ILogger<AbstractRabbitMQConsumer> _logger;

        /// <summary>
        /// Gets or sets the context.
        /// </summary>
        /// <value>
        /// The context.
        /// </value>
        protected RabbitMQConsumerContext? Context { get; set; }

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        protected abstract void Initialize();

        /// <summary>
        /// Initializes a new instance of the <see cref="AbstractRabbitMQConsumer"/> class.
        /// </summary>
        /// <param name="serviceProvider">The service provider.</param>
        /// <param name="logger">The logger.</param>
        /// <exception cref="ArgumentNullException">serviceProvider</exception>
        public AbstractRabbitMQConsumer(IServiceProvider serviceProvider, ILogger<AbstractRabbitMQConsumer> logger)
        {
            _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
            _logger = logger;
            Initialize();
        }

        /// <summary>
        /// Subscribes this instance.
        /// </summary>
        public void Subscribe()
        {
            using var scope = _serviceProvider.CreateScope();
            var options = scope.ServiceProvider.GetService<IOptionsSnapshot<RabbitMQOptions>>()?.Value;
            var factory = new ConnectionFactory()
            {
                //HostName = options?.HostName,
                UserName = options?.UserName,
                Password = options?.Password
            };
            if (options != null)
            {
                factory.Port = options.Port;
            }
            var connection = factory.CreateConnection(options.EndPoints);
            IModel _channel = connection.CreateModel();
            //分發模式
            _channel.ExchangeDeclare(Context?.ExchangeName, ExchangeType.Direct,true,false,null);
            _channel.QueueDeclare(queue: Context?.QueueName,
                                        durable: false,
                                        exclusive: false,
                                        autoDelete: false,
                                        arguments: null);
            _channel.QueueBind(Context?.QueueName, Context?.ExchangeName, Context?.RoutingKey);
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Process(message).ConfigureAwait(false).GetAwaiter().GetResult();
                _channel.BasicAck(ea.DeliveryTag, true);
            };
            _channel.BasicConsume(queue: Context?.QueueName, consumer: consumer);
        }

        /// <summary>
        /// 处理消息的方法
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        /// <exception cref="NotImplementedException"></exception>
        protected abstract Task Process(string message);
    }
}