using HKSH.Common.Enums;

namespace HKSH.Common.ShareModel.User
{
    /// <summary>
    /// UserInfo Page Response
    /// </summary>
    public class UserInfoPageResponse
    {
        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        /// <value>
        /// The user identifier.
        /// </value>
        public string UserId { get; set; } = null!;

        /// <summary>
        /// Gets or sets the user name en.
        /// </summary>
        /// <value>
        /// The user name en.
        /// </value>
        public string UserNameEn { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the user name tc.
        /// </summary>
        /// <value>
        /// The user name tc.
        /// </value>
        public string UserNameTc { get; set; } = string.Empty;

        /// <summary>
        /// Gets the name of the combo.
        /// UserNameEn (StaffId)
        /// </summary>
        /// <value>
        /// The name of the combo.
        /// </value>
        public string ComboName
        {
            get
            {
                return $"{UserNameEn} ({StaffId})";
            }
        }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>
        /// The title.
        /// </value>
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>
        /// The email.
        /// </value>
        public string? Email { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the phone.
        /// </summary>
        /// <value>
        /// The phone.
        /// </value>
        public string? Phone { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the photo.
        /// </summary>
        /// <value>
        /// The photo.
        /// </value>
        public string? Photo { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the staff identifier.
        /// </summary>
        /// <value>
        /// The staff identifier.
        /// </value>
        public string StaffId { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the patient identifier.
        /// </summary>
        /// <value>
        /// The patient identifier.
        /// </value>
        public string PatientId { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the doctor identifier.
        /// </summary>
        /// <value>
        /// The doctor identifier.
        /// </value>
        public string DoctorId { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the language.
        /// </summary>
        /// <value>
        /// The language.
        /// </value>
        public Language? Language { get; set; }

        /// <summary>
        /// Gets or sets the location code.
        /// </summary>
        /// <value>
        /// The location code.
        /// </value>
        public string LocationCode { get; set; } = string.Empty;

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
        /// Gets or sets the location alias.
        /// </summary>
        /// <value>
        /// The location alias.
        /// </value>
        public string? LocationAlias { get; set; }

        /// <summary>
        /// Gets or sets the department code.
        /// </summary>
        /// <value>
        /// The department code.
        /// </value>
        public string DepartmentCode { get; set; } = string.Empty;

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
        /// Gets or sets the department alias.
        /// </summary>
        /// <value>
        /// The department alias.
        /// </value>
        public string? DepartmentAlias { get; set; }

        /// <summary>
        /// Gets or sets the organization code.
        /// </summary>
        /// <value>
        /// The organization code.
        /// </value>
        public string OrganizationCode { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the organization name en.
        /// </summary>
        /// <value>
        /// The organization name en.
        /// </value>
        public string? OrganizationNameEn { get; set; }

        /// <summary>
        /// Gets or sets the organization name tc.
        /// </summary>
        /// <value>
        /// The organization name tc.
        /// </value>
        public string? OrganizationNameTc { get; set; }

        /// <summary>
        /// Gets or sets the organization short name en.
        /// </summary>
        /// <value>
        /// The organization short name en.
        /// </value>
        public string? OrganizationShortNameEn { get; set; }

        /// <summary>
        /// Gets or sets the organization short name tc.
        /// </summary>
        /// <value>
        /// The organization short name tc.
        /// </value>
        public string? OrganizationShortNameTc { get; set; }

        /// <summary>
        /// Gets or sets the site code.
        /// </summary>
        /// <value>
        /// The site code.
        /// </value>
        public string SiteCode { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the site full name en.
        /// </summary>
        /// <value>
        /// The site full name en.
        /// </value>
        public string SiteFullNameEn { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the site full name tc.
        /// </summary>
        /// <value>
        /// The site full name tc.
        /// </value>
        public string SiteFullNameTc { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the site short name en.
        /// </summary>
        /// <value>
        /// The site short name en.
        /// </value>
        public string SiteShortNameEn { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the site short name tc.
        /// </summary>
        /// <value>
        /// The site short name tc.
        /// </value>
        public string SiteShortNameTc { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the site alias.
        /// </summary>
        /// <value>
        /// The site alias.
        /// </value>
        public string SiteAlias { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>
        /// The status.
        /// </value>
        public ActiveStatus Status { get; set; }

        /// <summary>
        /// Gets or sets the lock.
        /// </summary>
        /// <value>
        /// The lock.
        /// </value>
        public LockStatus Lock { get; set; }

        /// <summary>
        /// Gets or sets the type of the role.
        /// </summary>
        /// <value>
        /// The type of the role.
        /// </value>
        public string RoleType { get; set; } = string.Empty;
    }
}