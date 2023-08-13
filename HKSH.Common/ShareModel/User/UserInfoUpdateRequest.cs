using HKSH.Common.Enums;

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
        public string UserId { get; set; } = null!;

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
        /// </summary>
        /// <value>
        /// The status.
        /// </value>
        public ActiveStatus? Status { get; set; }

        /// <summary>
        /// Gets or sets the lock.
        /// </summary>
        /// <value>
        /// The lock.
        /// </value>
        public LockStatus? Lock { get; set; }
    }
}