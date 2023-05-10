namespace HKSH.Common.RabbitMQ
{
    /// <summary>
    /// IMessageQueueConsumer
    /// </summary>
    public interface IMessageQueueConsumer
    {
        /// <summary>
        /// Subscribes this instance.
        /// </summary>
        void Subscribe();
    }
}