﻿//  Mou,Xiaohua 2023/05/17 15:54

namespace HKSH.Common.Repository.Database.Privileges
{
    /// <summary>
    /// menu permissions
    /// </summary>
    public class UserPrivilegeMenu
    {
        /// <summary>
        /// menu code
        /// </summary>
        public string MenuCode { get; set; } = String.Empty;

        /// <summary>
        /// parent menu code
        /// </summary>
        public string ParentMeuCode { get; set; } = String.Empty;

        /// <summary>
        /// menu type
        /// </summary>
        public int MenuType { get; set; }

        /// <summary>
        /// menu name
        /// </summary>
        public string MenuName { get; set; } = String.Empty;

        /// <summary>
        /// icon
        /// </summary>
        public string? Icon { get; set; }

        /// <summary>
        /// url
        /// </summary>
        public string? Url { get; set; }

        /// <summary>
        /// show on menu
        /// </summary>
        public bool? ShowOnMenu { get; set; }

        /// <summary>
        /// component
        /// </summary>
        public string? Component { get; set; }

        /// <summary>
        /// sub menus
        /// </summary>
        public List<UserPrivilegeMenu> SubMenus { get; set; } = new();

        /// <summary>
        /// access
        /// </summary>
        public List<UserPrivilegePermission> Accesses { get; set; } = new();
    }
}