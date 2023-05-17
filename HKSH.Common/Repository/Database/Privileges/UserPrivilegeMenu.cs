﻿//  Mou,Xiaohua 2023/05/17 15:54

using System;
using System.Collections.Generic;

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
        /// sub menus
        /// </summary>
        public List<UserPrivilegeMenu> SubMenus { get; set; } = new();

        /// <summary>
        /// access
        /// </summary>
        public List<UserPrivilegePermission> Accesses { get; set; } = new();
    }
}