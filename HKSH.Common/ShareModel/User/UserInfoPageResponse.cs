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