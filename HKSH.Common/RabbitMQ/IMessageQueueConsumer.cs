namespace HKSH.Common.RabbitMQ
{
    /// <summary>
    /// IMessage queue consumer
    /// </summary>
    public interface IMessageQueueConsumer
    {
        /// <summary>
        /// Subscribes this instance.
        /// </summary>
        void Subscribe();
    }
}