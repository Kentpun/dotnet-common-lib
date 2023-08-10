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
        public string DepartmentCode { get; set; } = null!;

        /// <summary>
        /// Gets or sets the department full name en.
        /// </summary>
        /// <value>
        /// The department full name en.
        /// </value>
        public string? DepartmentFullNameEn { get; set; }

        /// <summary>
        /// Gets or sets the department full name tc.
        /// </summary>
        /// <value>
        /// The department full name tc.
        /// </value>
        public string? DepartmentFullNameTc { get; set; }

        /// <summary>
        /// Gets or sets the department short name en.
        /// </summary>
        /// <value>
        /// The department short name en.
        /// </value>
        public string? DepartmentShortNameEn { get; set; }

        /// <summary>
        /// Gets or sets the department short name tc.
        /// </summary>
        /// <value>
        /// The department short name tc.
        /// </value>
        public string? DepartmentShortNameTc { get; set; }

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
        /// Gets or sets the department alias.
        /// </summary>
        /// <value>
        /// The department alias.
        /// </value>
        public string? DepartmentAlias { get; set; }

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
        /// Gets or sets the type of the department.
        /// </summary>
        /// <value>
        /// The type of the department.
        /// </value>
        public string? DepartmentType { get; set; }

        /// <summary>
        /// Gets or sets the phone.
        /// </summary>
        /// <value>
        /// The phone.
        /// </value>
        public string? Phone { get; set; }

        /// <summary>
        /// Gets or sets the fax.
        /// </summary>
        /// <value>
        /// The fax.
        /// </value>
        public string? Fax { get; set; }

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>
        /// The email.
        /// </value>
        public string? Email { get; set; }
    }
}