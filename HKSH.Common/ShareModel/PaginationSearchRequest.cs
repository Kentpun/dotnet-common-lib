using HKSH.Common.ShareModel;

namespace HKSH.EForm.Service.Models.Request
{
    /// <summary>
    /// PaginationSearchRequest
    /// </summary>
    /// <seealso cref="HKSH.Common.ShareModel.PaginationRequest" />
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
