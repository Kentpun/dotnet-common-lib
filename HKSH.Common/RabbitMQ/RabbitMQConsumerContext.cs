namespace HKSH.Common.RabbitMQ
{
    /// <summary>
    /// RabbitMQConsumerContext
    /// </summary>
    public class RabbitMQConsumerContext
    {
        /// <summary>
        /// Gets or sets the name of the exchange.
        /// </summary>
        /// <value>
        /// The name of the exchange.
        /// </value>
        public string? ExchangeName { get; set; }

        /// <summary>
        /// Gets or sets the routing key.
        /// </summary>
        /// <value>
        /// The routing key.
        /// </value>
        public string? RoutingKey { get; set; }

        /// <summary>
        /// Gets or sets the name of the queue.
        /// </summary>
        /// <value>
        /// The name of the queue.
        /// </value>
        public string? QueueName { get; set; }

        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        /// <value>
        /// The message.
        /// </value>
        public string? Message { get; set; }
    }
}