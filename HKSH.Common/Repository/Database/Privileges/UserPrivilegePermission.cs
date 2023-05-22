//  Mou,Xiaohua 2023/05/17 15:55

namespace HKSH.Common.Repository.Database.Privileges
{
    /// <summary>
    /// user permission
    /// </summary>
    public class UserPrivilegePermission
    {
        /// <summary>
        /// permission code
        /// </summary>
        public string PermissionCode { get; set; } = String.Empty;

        /// <summary>
        /// permission name
        /// </summary>
        public string PermissionName { get; set; } = String.Empty;

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