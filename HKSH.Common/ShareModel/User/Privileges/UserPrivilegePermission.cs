namespace HKSH.Common.ShareModel.User.Privileges
{
    /// <summary>
    /// user permission
    /// </summary>
    public class UserPrivilegePermission
    {
        /// <summary>
        /// permission code
        /// </summary>
        public string PermissionCode { get; set; } = string.Empty;

        /// <summary>
        /// permission name
        /// </summary>
        public string PermissionName { get; set; } = string.Empty;

        /// <summary>
        /// has read
        /// </summary>
        public bool? HasRead { get; set; }

        /// <summary>
        /// has write
        /// </summary>
        public bool? HasWrite { get; set; }
    }
}