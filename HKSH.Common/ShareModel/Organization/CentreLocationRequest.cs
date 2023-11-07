using HKSH.Common.ShareModel.Request;

namespace HKSH.Common.ShareModel.Organization
{
    /// <summary>
    /// CentreLocationRequest
    /// </summary>
    /// <seealso cref="KeywordPageRequest" />
    public class CentreLocationRequest : KeywordPageRequest
    {
        /// <summary>
        /// Gets or sets the site code.
        /// </summary>
        /// <value>
        /// The site code.
        /// </value>
        public string? SiteCode { get; set; }

        /// <summary>
        /// Gets or sets the location codes.
        /// </summary>
        /// <value>
        /// The location codes.
        /// </value>
        public List<string>? LocationCodes { get; set; }
    }
}
