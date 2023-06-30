using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using System.Text;

namespace HKSH.Common.RabbitMQ
{
    /// <summary>
    /// RabbitMQ client
    /// </summary>
    public class RabbitMQPublisher : IRabbitMQPublisher
    {
        /// <summary>
        /// The channel
        /// </summary>
        private IModel Channel { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="RabbitMQPublisher"/> class.
        /// </summary>
        /// <param name="options">The options.</param>
        public RabbitMQPublisher(IOptions<RabbitMQOptions> options)
        {
            var factory = new ConnectionFactory()
            {
                //HostName = options.Value.HostName,
                UserName = options.Value.UserName,
                Password = options.Value.Password,
                Port = options.Value.Port,
            };
            var connection = factory.CreateConnection(options.Value.EndPoints);
            Channel = connection.CreateModel();
        }

        /// <summary>
        /// Pushes the message.
        /// </summary>
        /// <param name="exchangeName">Name of the exchange.</param>
        /// <param name="queueName">Name of the queue.</param>
        /// <param name="routingKey">The routing key.</param>
        /// <param name="message">The message.</param>
        public void PushMessage(RabbitMQConsumerContext context)
        {
            //把交换机设置成分發模式
            Channel.ExchangeDeclare(context.ExchangeName, ExchangeType.Direct, true, false, null);
            Channel.QueueDeclare(queue: context.QueueName,
                                        durable: false,
                                        exclusive: false,
                                        autoDelete: false,
                                        arguments: null);
            Channel.QueueBind(context.QueueName, context.ExchangeName, context.RoutingKey);
            var body = Encoding.UTF8.GetBytes(context.Message ?? string.Empty);
            Channel.BasicPublish(exchange: context.ExchangeName,
                                    routingKey: context.RoutingKey,
                                    basicProperties: null,
                                    body: body);
        }
    }
}