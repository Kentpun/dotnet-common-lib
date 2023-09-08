﻿namespace HKSH.Common.ShareModel.User.Privileges
{
    /// <summary>
    /// user permission module
    /// </summary>
    public class UserPrivilegeModule
    {
        /// <summary>
        /// module code
        /// </summary>
        public string ModuleCode { get; set; } = string.Empty;

        /// <summary>
        /// module name
        /// </summary>
        public string ModuleName { get; set; } = string.Empty;

        /// <summary>
        /// role code
        /// </summary>
        public List<string> RoleCodes { get; set; } = new();

        /// <summary>
        /// role name
        /// </summary>
        public List<string> RoleNames { get; set; } = new();

        /// <summary>
        /// location code
        /// </summary>
        public string LocationCode { get; set; } = string.Empty;

        /// <summary>
        /// location name
        /// </summary>
        public string LocationName { get; set; } = string.Empty;

        /// <summary>
        /// icon
        /// </summary>
        public string Icon { get; set; } = string.Empty;

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