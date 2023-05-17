﻿//  Mou,Xiaohua 2023/05/17 15:50

using System;
using System.Collections.Generic;

namespace HKSH.Common.Repository.Database.Privileges
{
    /// <summary>
    /// user permission module
    /// </summary>
    public class UserPrivilegeModule
    {
        /// <summary>
        /// module code
        /// </summary>
        public string ModuleCode { get; set; } = String.Empty;

        /// <summary>
        /// role code
        /// </summary>
        public string RoleCode { get; set; } = String.Empty;

        /// <summary>
        /// location code
        /// </summary>
        public string LocationCode { get; set; } = String.Empty;

        /// <summary>
        /// centre menus
        /// </summary>
        public List<UserPrivilegeMenu> CentreMenus { get; set; } = new();

        /// <summary>
        /// patient menus
        /// </summary>
        public List<UserPrivilegeMenu> PatientMenus { get; set; } = new();

        /// <summary>
        /// patient type setting
        /// </summary>
        public List<UserPrivilegeMenu> PatientTypeSettingMenus { get; set; } = new();
    }
}