using HKSH.Common.Enums;

namespace HKSH.Common.ShareModel.User
{
    /// <summary>
    /// UserInfoResponse
    /// </summary>
    public class UserInfoResponse
    {
        /// <summary>
        /// Gets or sets the staff identifier.
        /// </summary>
        /// <value>
        /// The staff identifier.
        /// </value>
        public string StaffId { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the user name en.
        /// </summary>
        /// <value>
        /// The user name en.
        /// </value>
        public string UserNameEn { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the user name cn.
        /// </summary>
        /// <value>
        /// The user name cn.
        /// </value>
        public string UserNameCn { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the phone.
        /// </summary>
        /// <value>
        /// The phone.
        /// </value>
        public string? Phone { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>
        /// The email.
        /// </value>
        public string? Email { get; set; } = string.Empty;

        /// <summary>
        /// language
        /// </summary>
        public Language? Language { get; set; }
    }
}