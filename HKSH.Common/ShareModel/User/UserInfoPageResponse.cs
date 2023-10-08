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
        /// Gets or sets the user default locations.
        /// </summary>
        /// <value>
        /// The user default locations.
        /// </value>
        public List<UserDefaultLocationResponse> UserDefaultLocations { get; set; } = new();
    }
}