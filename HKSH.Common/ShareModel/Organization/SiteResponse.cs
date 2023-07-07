namespace HKSH.Common.ShareModel.Organization
{
    /// <summary>
    /// Site Response
    /// </summary>
    public class SiteResponse
    {
        /// <summary>
        /// Gets or sets the site code.
        /// </summary>
        /// <value>
        /// The site code.
        /// </value>
        public string? SiteCode { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the site sort order.
        /// </summary>
        /// <value>
        /// The site sort order.
        /// </value>
        public decimal? SiteSortOrder { get; set; }

        /// <summary>
        /// Gets or sets the site full name en.
        /// </summary>
        /// <value>
        /// The site full name en.
        /// </value>
        public string? SiteFullNameEn { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the site full name tc.
        /// </summary>
        /// <value>
        /// The site full name tc.
        /// </value>
        public string? SiteFullNameTc { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the site short name en.
        /// </summary>
        /// <value>
        /// The site short name en.
        /// </value>
        public string? SiteShortNameEn { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the site short name tc.
        /// </summary>
        /// <value>
        /// The site short name tc.
        /// </value>
        public string? SiteShortNameTc { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the site alias.
        /// </summary>
        /// <value>
        /// The site alias.
        /// </value>
        public string? SiteAlias { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the address.
        /// </summary>
        /// <value>
        /// The address.
        /// </value>
        public string? Address { get; set; } = string.Empty;
    }
}