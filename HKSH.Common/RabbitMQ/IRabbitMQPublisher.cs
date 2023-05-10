namespace HKSH.Common.RabbitMQ
{
    /// <summary>
    /// IRabbitMQPublisher
    /// </summary>
    public interface IRabbitMQPublisher
    {
        /// <summary>
        /// Pushes the message.
        /// </summary>
        /// <param name="context">The context.</param>
        void PushMessage(RabbitMQConsumerContext context);
    }
}