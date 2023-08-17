namespace HKSH.Common.ShareModel.Organization
{
    /// <summary>
    /// LocationTypeTreeResponse
    /// </summary>
    public class LocationTypeTreeResponse
    {
        /// <summary>
        /// Gets or sets the location type identifier.
        /// </summary>
        /// <value>
        /// The location type identifier.
        /// </value>
        public int LocationTypeId { get; set; }

        /// <summary>
        /// Gets or sets the location type code.
        /// </summary>
        /// <value>
        /// The location type code.
        /// </value>
        public string LocationTypeCode { get; set; } = null!;

        /// <summary>
        /// Gets or sets the location type description en.
        /// </summary>
        /// <value>
        /// The location type description en.
        /// </value>
        public string LocationTypeDescriptionEn { get; set; } = null!;

        /// <summary>
        /// Gets or sets the location type description tc.
        /// </summary>
        /// <value>
        /// The location type description tc.
        /// </value>
        public string LocationTypeDescriptionTc { get; set; } = null!;

        /// <summary>
        /// Gets or sets the location type alias.
        /// </summary>
        /// <value>
        /// The location type alias.
        /// </value>
        public string LocationTypeAlias { get; set; } = null!;

        /// <summary>
        /// Gets or sets the locations.
        /// </summary>
        /// <value>
        /// The locations.
        /// </value>
        public List<LocationTypeTreeLocationResponse>? Locations { get; set; }
    }

    /// <summary>
    /// LocationTypeTreeLocationResponse
    /// </summary>
    public class LocationTypeTreeLocationResponse
    {
        /// <summary>
        /// Gets or sets the location identifier.
        /// </summary>
        /// <value>
        /// The location identifier.
        /// </value>
        public int LocationId { get; set; }

        /// <summary>
        /// Gets or sets the location code.
        /// </summary>
        /// <value>
        /// The location code.
        /// </value>
        public string LocationCode { get; set; } = null!;

        /// <summary>
        /// Gets or sets the location full name en.
        /// </summary>
        /// <value>
        /// The location full name en.
        /// </value>
        public string? LocationFullNameEn { get; set; } = null!;

        /// <summary>
        /// Gets or sets the location full name tc.
        /// </summary>
        /// <value>
        /// The location full name tc.
        /// </value>
        public string? LocationFullNameTc { get; set; } = null!;

        /// <summary>
        /// Gets or sets the location short name en.
        /// </summary>
        /// <value>
        /// The location short name en.
        /// </value>
        public string? LocationShortNameEn { get; set; } = null!;

        /// <summary>
        /// Gets or sets the location alias.
        /// </summary>
        /// <value>
        /// The location alias.
        /// </value>
        public string? LocationAlias { get; set; } = null!;
    }
}