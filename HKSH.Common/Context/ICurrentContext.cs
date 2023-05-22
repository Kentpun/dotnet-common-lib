using System.Collections.Generic;
using HKSH.Common.Repository.Database;
using HKSH.Common.Repository.Database.Privileges;

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
        long CurrentUserId { get; }

        /// <summary>
        /// Gets the current user.
        /// </summary>
        /// <value>
        /// The current user.
        /// </value>
        ClaimCurrentUser CurrentUser { get; }

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