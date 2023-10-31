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

        /// <summary>
        /// Gets or sets the user default locations.
        /// </summary>
        /// <value>
        /// The user default locations.
        /// </value>
        public List<UserDefaultLocationResponse> UserDefaultLocations { get; set; } = new();

        /// <summary>
        /// Gets or sets the person.
        /// </summary>
        /// <value>
        /// The person.
        /// </value>
        public PersonResponse? Person { get; set; }
    }
}