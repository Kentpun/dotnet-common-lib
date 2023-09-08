namespace HKSH.Common.ShareModel.Request
{
    /// <summary>
    /// PaginationSearchRequest
    /// </summary>
    /// <seealso cref="PaginationRequest" />
    public class PaginationSearchRequest : PaginationRequest
    {
        /// <summary>
        /// Gets or sets the keyword.
        /// </summary>
        /// <value>
        /// The keyword.
        /// </value>
        public string? Keyword { get; set; }
    }
}