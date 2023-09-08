using HKSH.Common.ShareModel.User;
using HKSH.Common.ShareModel.User.Privileges;

namespace HKSH.Common.Context
{
    /// <summary>
    /// ICurrentContext
    /// </summary>
    public interface ICurrentContext
    {
        /// <summary>
        /// current user id
        /// </summary>
        string CurrentUserId { get; }

        /// <summary>
        /// Gets the current user.
        /// </summary>
        /// <value>
        /// The current user.
        /// </value>
        UserInfoResponse CurrentUser { get; }

        /// <summary>
        /// current location
        /// </summary>
        string? CurrentLocation { get; }

        /// <summary>
        /// user permissions
        /// </summary>
        List<UserPrivilegeModule> UserPermissions { get; }
    }
}