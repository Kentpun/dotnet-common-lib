namespace HKSH.Common.ShareModel.User
{
    /// <summary>
    /// UserInfo List Request
    /// </summary>
    public class UserInfoListRequest
    {
        /// <summary>
        /// Gets or sets the keyword.
        /// </summary>
        /// <value>
        /// The keyword.
        /// </value>
        public string? Keyword { get; set; }

        /// <summary>
        /// Gets or sets the user ids.
        /// </summary>
        /// <value>
        /// The user ids.
        /// </value>
        public List<string>? UserIds { get; set; }

        /// <summary>
        /// Gets or sets the exclude user ids.
        /// </summary>
        /// <value>
        /// The exclude user ids.
        /// </value>
        public List<string>? ExcludeUserIds { get; set; }

        /// <summary>
        /// Gets or sets the staff ids.
        /// </summary>
        /// <value>
        /// The staff ids.
        /// </value>
        public List<string>? StaffIds { get; set; }

        /// <summary>
        /// Gets or sets the doctor ids.
        /// </summary>
        /// <value>
        /// The doctor ids.
        /// </value>
        public List<string>? DoctorIds { get; set; }

        /// <summary>
        /// Gets or sets the patient ids.
        /// </summary>
        /// <value>
        /// The patient ids.
        /// </value>
        public List<string>? PatientIds { get; set; }

        /// <summary>
        /// Gets or sets the group.
        /// </summary>
        /// <value>
        /// The group.
        /// </value>
        public string? Group { get; set; }

        /// <summary>
        /// Gets or sets the location code.
        /// </summary>
        /// <value>
        /// The location code.
        /// </value>
        public string? LocationCode { get; set; }

        /// <summary>
        /// Gets or sets the department code.
        /// </summary>
        /// <value>
        /// The department code.
        /// </value>
        public string? DepartmentCode { get; set; }

        /// <summary>
        /// Gets or sets the type of the role.
        /// </summary>
        /// <value>
        /// The type of the role.
        /// </value>
        public string? RoleType { get; set; }
    }
}