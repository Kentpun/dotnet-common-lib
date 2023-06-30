namespace HKSH.Common.RabbitMQ
{
    /// <summary>
    /// RabbitMQ options
    /// </summary>
    public class RabbitMQOptions
    {
        /// <summary>
        /// Gets or sets the name of the host.
        /// </summary>
        /// <value>
        /// The name of the host.
        /// </value>
        public string? HostName { get; set; }

        /// <summary>
        /// Gets or sets the connection string.
        /// </summary>
        /// <value>
        /// The connection string.
        /// </value>
        public string? UserName { get; set; }

        /// <summary>
        /// Password
        /// </summary>
        /// <value>
        /// The password.
        /// </value>
        public string? Password { get; set; }

        /// <summary>
        /// redis cluster EndPoints
        /// </summary>
        /// <value>
        /// The end points.
        /// </value>
        public List<string> EndPoints { get; set; } = new List<string>();

        /// <summary>
        /// Port
        /// </summary>
        /// <value>
        /// The port.
        /// </value>
        public int Port { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="RabbitMQOptions"/> is enable.
        /// </summary>
        /// <value>
        ///   <c>true</c> if enable; otherwise, <c>false</c>.
        /// </value>
        public bool Enable { get; set; }
    }
}