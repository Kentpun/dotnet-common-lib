namespace HKSH.Common.ShareModel.Organization
{
    /// <summary>
    /// Location Response
    /// </summary>
    public class LocationResponse
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
        public string? LocationFullNameEn { get; set; }

        /// <summary>
        /// Gets or sets the location full name tc.
        /// </summary>
        /// <value>
        /// The location full name tc.
        /// </value>
        public string? LocationFullNameTc { get; set; }

        /// <summary>
        /// Gets or sets the location short name en.
        /// </summary>
        /// <value>
        /// The location short name en.
        /// </value>
        public string? LocationShortNameEn { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string? Description { get; set; }

        /// <summary>
        /// Gets or sets the location alias.
        /// </summary>
        /// <value>
        /// The location alias.
        /// </value>
        public string? LocationAlias { get; set; }

        /// <summary>
        /// Gets or sets the correspondence address en.
        /// </summary>
        /// <value>
        /// The correspondence address en.
        /// </value>
        public string? CorrespondenceAddressEn { get; set; }

        /// <summary>
        /// Gets or sets the correspondence address tc.
        /// </summary>
        /// <value>
        /// The correspondence address tc.
        /// </value>
        public string? CorrespondenceAddressTc { get; set; }

        /// <summary>
        /// Gets or sets the address.
        /// </summary>
        /// <value>
        /// The address.
        /// </value>
        public string? Address { get; set; }

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
        /// Gets or sets the site code.
        /// </summary>
        /// <value>
        /// The site code.
        /// </value>
        public string SiteCode { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the site short name en.
        /// </summary>
        /// <value>
        /// The site short name en.
        /// </value>
        public string SiteShortNameEn { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the organization.
        /// </summary>
        /// <value>
        /// The organization.
        /// </value>
        public OrganizationResponse? Organization { get; set; }

        /// <summary>
        /// Gets or sets the department.
        /// </summary>
        /// <value>
        /// The department.
        /// </value>
        public DepartmentResponse? Department { get; set; }

        /// <summary>
        /// Gets or sets the site.
        /// </summary>
        /// <value>
        /// The site.
        /// </value>
        public SiteResponse? Site { get; set; }
    }
}