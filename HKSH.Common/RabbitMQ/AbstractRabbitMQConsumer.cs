﻿using HKSH.Common.Extensions;
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
            using IServiceScope scope = _serviceProvider.CreateScope();
            RabbitMQOptions? options = scope.ServiceProvider.GetService<IOptionsSnapshot<RabbitMQOptions>>()?.Value;
            ConnectionFactory factory = new ConnectionFactory()
            {
                //HostName = options?.HostName,
                UserName = options?.UserName,
                Password = options?.Password
            };
            if (options != null)
            {
                factory.Port = options.Port;
            }
            IConnection connection = factory.CreateConnection(options.EndPoints);
            IModel _channel = connection.CreateModel();
            //分發模式
            _channel.ExchangeDeclare(Context?.ExchangeName, ExchangeType.Direct, true, false, null);
            _channel.QueueDeclare(queue: Context?.QueueName,
                                        durable: false,
                                        exclusive: false,
                                        autoDelete: false,
                                        arguments: null);
            _channel.QueueBind(Context?.QueueName, Context?.ExchangeName, Context?.RoutingKey);
            EventingBasicConsumer consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (model, ea) =>
            {
                try
                {
                    byte[] body = ea.Body.ToArray();
                    string message = Encoding.UTF8.GetString(body);
                    Process(message).ConfigureAwait(false).GetAwaiter().GetResult();
                    _channel.BasicAck(ea.DeliveryTag, true);
                }
                catch (Exception e)
                {
                    HandleException(model, ea, e, _channel);
                }
            };
            _channel.BasicConsume(queue: Context?.QueueName, consumer: consumer);
        }

        /// <summary>
        /// handle exception
        /// </summary>
        /// <param name="model"></param>
        /// <param name="ea"></param>
        /// <param name="ex"></param>
        /// <param name="_channel"></param>
        protected virtual void HandleException(object? model, BasicDeliverEventArgs ea, Exception ex, IModel _channel)
        {
            _logger.LogExc(ex);
            _channel.BasicNack(ea.DeliveryTag, false, false);
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