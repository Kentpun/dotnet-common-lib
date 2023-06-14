//  Mou,Xiaohua 2023/05/17 15:50

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
        public string LocationCode { get; set; } = String.Empty;

        /// <summary>
        /// location name
        /// </summary>
        public string LocationName { get; set; } = String.Empty;

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