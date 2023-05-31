namespace HKSH.Common.AuditLogs.Models
{
    /// <summary>
    /// Log Mq Request
    /// </summary>
    public class LogMqRequest
    {
        /// <summary>
        /// Gets or sets the UUID.
        /// </summary>
        /// <value>
        /// The UUID.
        /// </value>
        public Guid Uuid { get; set; }

        /// <summary>
        /// Gets or sets the action.
        /// </summary>
        /// <value>
        /// The action.
        /// </value>
        public string Action { get; set; } = null!;

        /// <summary>
        /// Gets or sets the log.
        /// </summary>
        /// <value>
        /// The log.
        /// </value>
        public object Log { get; set; } = null!;
    }
}