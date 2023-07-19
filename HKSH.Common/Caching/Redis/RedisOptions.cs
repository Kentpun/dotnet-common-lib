namespace HKSH.Common.Caching.Redis
{
    /// <summary>
    /// Redis Options
    /// </summary>
    public class RedisOptions
    {
        /// <summary>
        /// Gets or sets the name of the user.
        /// </summary>
        /// <value>
        /// The name of the user.
        /// </value>
        public string? UserName { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>
        /// The password.
        /// </value>
        public string? Password { get; set; }

        /// <summary>
        /// Gets or sets the end points.
        /// </summary>
        /// <value>
        /// The end points.
        /// </value>
        public List<string> EndPoints { get; set; } = new();

        /// <summary>
        /// Gets or sets the port.
        /// </summary>
        /// <value>
        /// The port.
        /// </value>
        public int Port { get; set; }

        /// <summary>
        /// Gets or sets the connection string.
        /// </summary>
        /// <value>
        /// The connection string.
        /// </value>
        public string? ConnectionString { get; set; }
    }
}