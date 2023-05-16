namespace HKSH.Common.AuditLogs
{
    /// <summary>
    /// AuditLogRequest
    /// </summary>
    public class AuditLogRequest
    {
        /// <summary>
        /// Gets or sets the UUID.
        /// </summary>
        /// <value>
        /// The UUID.
        /// </value>
        public string? Uuid { get; set; }

        /// <summary>
        /// Gets or sets the business code.
        /// </summary>
        /// <value>
        /// The business code.
        /// </value>
        public string? BusinessCode { get; set; }
    }
}
