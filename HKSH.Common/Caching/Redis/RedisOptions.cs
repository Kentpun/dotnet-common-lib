namespace HKSH.Common.Caching.Redis
{
    /// <summary>
    /// Redis Options
    /// </summary>
    public class RedisOptions
    {
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
        public string? Password { get; set; }

        /// <summary>
        /// redis cluster EndPoints
        /// </summary>
        public List<string> EndPoints { get; set; } = new List<string>();

        /// <summary>
        /// Port
        /// </summary>
        public int Port { get; set; }

        /// <summary>
        /// ConnectionString
        /// </summary>
        public string? ConnectionString { get; set; }

    }
}