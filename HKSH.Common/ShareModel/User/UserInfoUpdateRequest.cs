namespace HKSH.Common.ShareModel.User
{
    /// <summary>
    /// UserInfo Update Request
    /// </summary>
    public class UserInfoUpdateRequest
    {
        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        /// <value>
        /// The user identifier.
        /// </value>
        public string? UserId { get; set; }

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
        public string? LocationCode { get; set; }

        /// <summary>
        /// Gets or sets the type of the role.
        /// </summary>
        /// <value>
        /// The type of the role.
        /// </value>
        public string? RoleType { get; set; }

        /// <summary>
        /// Gets or sets the status.
        /// Source HKSH.Common/Enums/ActiveStatus
        /// </summary>
        /// <value>
        /// The status.
        /// </value>
        public string? Status { get; set; }

        /// <summary>
        /// Gets or sets the lock.
        /// Source HKSH.Common/Enums/LockStatus
        /// </summary>
        /// <value>
        /// The lock.
        /// </value>
        public string? Lock { get; set; }
    }
}