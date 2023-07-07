namespace HKSH.Common.ShareModel.Organization
{
    /// <summary>
    /// Department Response
    /// </summary>
    public class DepartmentResponse
    {
        /// <summary>
        /// Gets or sets the department identifier.
        /// </summary>
        /// <value>
        /// The department identifier.
        /// </value>
        public int DepartmentId { get; set; }

        /// <summary>
        /// Gets or sets the department code.
        /// </summary>
        /// <value>
        /// The department code.
        /// </value>
        public string? DepartmentCode { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the department sort order.
        /// </summary>
        /// <value>
        /// The department sort order.
        /// </value>
        public decimal? DepartmentSortOrder { get; set; }

        /// <summary>
        /// Gets or sets the department full name en.
        /// </summary>
        /// <value>
        /// The department full name en.
        /// </value>
        public string? DepartmentFullNameEn { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the department full name tc.
        /// </summary>
        /// <value>
        /// The department full name tc.
        /// </value>
        public string? DepartmentFullNameTc { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the department short name en.
        /// </summary>
        /// <value>
        /// The department short name en.
        /// </value>
        public string? DepartmentShortNameEn { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the department short name tc.
        /// </summary>
        /// <value>
        /// The department short name tc.
        /// </value>
        public string? DepartmentShortNameTc { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the correspondence address en.
        /// </summary>
        /// <value>
        /// The correspondence address en.
        /// </value>
        public string? CorrespondenceAddressEn { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the correspondence address tc.
        /// </summary>
        /// <value>
        /// The correspondence address tc.
        /// </value>
        public string? CorrespondenceAddressTc { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the department alias.
        /// </summary>
        /// <value>
        /// The department alias.
        /// </value>
        public string? DepartmentAlias { get; set; } = string.Empty;
    }
}