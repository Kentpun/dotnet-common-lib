namespace HKSH.Common.ShareModel.Response
{
    /// <summary>
    /// ActionResponse
    /// </summary>
    public class ActionResponse<T>
    {
        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        public T? Value { get; set; }

        /// <summary>
        /// Gets or sets the status code.
        /// </summary>
        /// <value>
        /// The status code.
        /// </value>
        public int? StatusCode { get; set; }
    }
}