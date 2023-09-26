using HKSH.Common.Constants;
using HKSH.Common.ShareModel.User.Privileges;

namespace HKSH.Common.ShareModel.User
{
    /// <summary>
    /// UserInfo Response
    /// </summary>
    public class UserInfoResponse
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
        /// Gets or sets the display in signature.
        /// </summary>
        /// <value>
        /// The display in signature.
        /// </value>
        public string? DisplayInSignature { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the domain login.
        /// </summary>
        /// <value>
        /// The domain login.
        /// </value>
        public string DomainLogin { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the last login.
        /// </summary>
        /// <value>
        /// The last login.
        /// </value>
        public DateTime? LastLogin { get; set; }

        /// <summary>
        /// Gets or sets the language.
        /// Source HKSH.Common/Enums/Language
        /// </summary>
        /// <value>
        /// The language.
        /// </value>
        public string? Language { get; set; }

        /// <summary>
        /// Gets or sets the location code.
        /// </summary>
        /// <value>
        /// The location code.
        /// </value>
        public string LocationCode { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the location identifier.
        /// </summary>
        /// <value>
        /// The location identifier.
        /// </value>
        public int? LocationId { get; set; }

        /// <summary>
        /// Gets or sets the status.
        /// Source HKSH.Common/Enums/ActiveStatus
        /// </summary>
        /// <value>
        /// The status.
        /// </value>
        public string Status { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the lock.
        /// Source HKSH.Common/Enums/LockStatus
        /// </summary>
        /// <value>
        /// The lock.
        /// </value>
        public string Lock { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the type of the role.
        /// </summary>
        /// <value>
        /// The type of the role.
        /// </value>
        public string RoleType { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the type of the location.
        /// </summary>
        /// <value>
        /// The type of the location.
        /// </value>
        public string? LocationType { get; set; }

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
        /// Gets or sets the name of the patient.
        /// </summary>
        /// <value>
        /// The name of the patient.
        /// </value>
        public string? PatientName { get; set; }

        /// <summary>
        /// Gets or sets the doctor name en.
        /// </summary>
        /// <value>
        /// The doctor name en.
        /// </value>
        public string? DoctorNameEn { get; set; }

        /// <summary>
        /// Gets or sets the doctor name tc.
        /// </summary>
        /// <value>
        /// The doctor name tc.
        /// </value>
        public string? DoctorNameTc { get; set; }

        /// <summary>
        /// permissions
        /// </summary>
        public List<UserPrivilegeModule> Permissions { get; set; } = new();

        /// <summary>
        /// user roles
        /// </summary>
        public List<string> UserRoles { get; set; } = new();

        /// <summary>
        /// has role
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        public bool HasRole(string role)
        {
            return UserRoles.Contains(role);
        }

        /// <summary>
        /// is admin user
        /// </summary>
        /// <returns></returns>
        public bool IsAdminUser()
        {
            return HasRole(CommonRoleConstants.ADMIN_ROLE_CODE) || HasRole(CommonRoleConstants.ADMIN_ROLE_CODE1);
        }
    }
}