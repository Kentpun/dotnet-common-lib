namespace HKSH.Common.ShareModel.Request
{
    /// <summary>
    /// ApiRequest
    /// </summary>
    public class ApiRequest
    {
        /// <summary>
        /// Gets or sets the trace identifier.
        /// </summary>
        /// <value>
        /// The trace identifier.
        /// </value>
        public string? TraceId { get; set; }

        /// <summary>
        /// Gets or sets the span identifier.
        /// </summary>
        /// <value>
        /// The span identifier.
        /// </value>
        public string? SpanId { get; set; }

        /// <summary>
        /// Gets or sets the URL.
        /// </summary>
        /// <value>
        /// The URL.
        /// </value>
        public string? Url { get; set; }

        /// <summary>
        /// Gets or sets the method.
        /// </summary>
        /// <value>
        /// The method.
        /// </value>
        public string? Method { get; set; }

        /// <summary>
        /// Gets or sets the parameters.
        /// </summary>
        /// <value>
        /// The parameters.
        /// </value>
        public string? Parameters { get; set; }

        /// <summary>
        /// Gets or sets the response.
        /// </summary>
        /// <value>
        /// The response.
        /// </value>
        public string? Response { get; set; }

        /// <summary>
        /// Gets or sets the request time.
        /// </summary>
        /// <value>
        /// The request time.
        /// </value>
        public DateTime? RequestTime { get; set; }

        /// <summary>
        /// Gets or sets the response time.
        /// </summary>
        /// <value>
        /// The response time.
        /// </value>
        public DateTime? ResponseTime { get; set; }

        /// <summary>
        /// Gets or sets the execution time.
        /// </summary>
        /// <value>
        /// The execution time.
        /// </value>
        public double ExecutionTime { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is success.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is success; otherwise, <c>false</c>.
        /// </value>
        public bool IsSuccess { get; set; }

        /// <summary>
        /// Gets or sets the error.
        /// </summary>
        /// <value>
        /// The error.
        /// </value>
        public string? Error { get; set; }
    }
}