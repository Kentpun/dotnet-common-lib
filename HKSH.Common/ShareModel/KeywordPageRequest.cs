using HKSH.Common.ShareModel;

namespace HKSH.Common.ShareModel
{
    /// <summary>
    /// KeywordPageRequest
    /// </summary>
    /// <seealso cref="PaginationRequest" />
    public class KeywordPageRequest : PaginationRequest
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