namespace HKSH.Common.ShareModel.Organization
{
    /// <summary>
    /// Location Type Response
    /// </summary>
    public class LocationTypeResponse
    {
        /// <summary>
        /// Gets or sets the location type description en.
        /// </summary>
        /// <value>
        /// The location type description en.
        /// </value>
        public string? LocationTypeDescriptionEn { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the location type description tc.
        /// </summary>
        /// <value>
        /// The location type description tc.
        /// </value>
        public string? LocationTypeDescriptionTc { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the location type alias.
        /// </summary>
        /// <value>
        /// The location type alias.
        /// </value>
        public string? LocationTypeAlias { get; set; } = string.Empty;
    }
}