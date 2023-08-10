namespace HKSH.Common.ShareModel.Organization
{
    /// <summary>
    /// Organization Response
    /// </summary>
    public class OrganizationResponse
    {
        /// <summary>
        /// Gets or sets the organization code.
        /// </summary>
        /// <value>
        /// The organization code.
        /// </value>
        public string OrganizationCode { get; set; } = null!;

        /// <summary>
        /// Gets or sets the organization name en.
        /// </summary>
        /// <value>
        /// The organization name en.
        /// </value>
        public string OrganizationNameEn { get; set; } = null!;

        /// <summary>
        /// Gets or sets the organization name tc.
        /// </summary>
        /// <value>
        /// The organization name tc.
        /// </value>
        public string OrganizationNameTc { get; set; } = null!;

        /// <summary>
        /// Gets or sets the organization short name en.
        /// </summary>
        /// <value>
        /// The organization short name en.
        /// </value>
        public string OrganizationShortNameEn { get; set; } = null!;

        /// <summary>
        /// Gets or sets the organization short name tc.
        /// </summary>
        /// <value>
        /// The organization short name tc.
        /// </value>
        public string OrganizationShortNameTc { get; set; } = null!;

        /// <summary>
        /// Gets or sets the logos.
        /// </summary>
        /// <value>
        /// The logos.
        /// </value>
        public List<OrganizationLogoResponse>? Logos { get; set; }
    }
}