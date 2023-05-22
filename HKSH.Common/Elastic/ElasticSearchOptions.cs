namespace HKSH.Common.RabbitMQ
{
    /// <summary>
    /// ElasticSearchOptions
    /// </summary>
    public class ElasticSearchOptions
    {
        /// <summary>
        /// Gets or sets the URL.
        /// </summary>
        /// <value>
        /// The URL.
        /// </value>
        public string? Url { get; set; }

        /// <summary>
        /// Gets or sets the default index.
        /// </summary>
        /// <value>
        /// The default index.
        /// </value>
        public string? DefaultIndex { get; set; }

        /// <summary>
        /// Gets or sets the certificate fingerprint.
        /// </summary>
        /// <value>
        /// The certificate fingerprint.
        /// </value>
        public string? CertificateFingerprint { get; set; }

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
    }
}