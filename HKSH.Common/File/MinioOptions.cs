namespace HKSH.Common.File
{
    /// <summary>
    /// MinioOptions
    /// </summary>
    public class MinioOptions
    {
        /// <summary>
        /// The section
        /// </summary>
        public const string SECTION = "MinioOptions";

        /// <summary>
        /// Gets or sets the endpoint.
        /// </summary>
        /// <value>
        /// The endpoint.
        /// </value>
        public string Endpoint { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the client identifier.
        /// </summary>
        /// <value>
        /// The client identifier.
        /// </value>
        public string ClientId { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the client secret.
        /// </summary>
        /// <value>
        /// The client secret.
        /// </value>
        public string ClientSecret { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the name of the bucket.
        /// </summary>
        /// <value>
        /// The name of the bucket.
        /// </value>
        public string BucketName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the presigned expires.
        /// </summary>
        /// <value>
        /// The presigned expires.
        /// </value>
        public int PresignedExpires { get; set; }
    }
}
