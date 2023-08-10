namespace HKSH.Common.ShareModel.Organization
{
    /// <summary>
    /// Organization Logo Response
    /// </summary>
    public class OrganizationLogoResponse
    {
        /// <summary>
        /// Gets or sets the type of the logo.
        /// </summary>
        /// <value>
        /// The type of the logo.
        /// </value>
        public string LogoType { get; set; } = null!;

        /// <summary>
        /// Gets or sets the logo.
        /// </summary>
        /// <value>
        /// The logo.
        /// </value>
        public string Logo { get; set; } = null!;
    }
}