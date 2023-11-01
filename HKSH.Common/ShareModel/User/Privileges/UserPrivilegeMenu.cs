namespace HKSH.Common.ShareModel.User.Privileges
{
    /// <summary>
    /// menu permissions
    /// </summary>
    public class UserPrivilegeMenu
    {
        /// <summary>
        /// menu code
        /// </summary>
        public string MenuCode { get; set; } = string.Empty;

        /// <summary>
        /// parent menu code
        /// </summary>
        public string ParentMeuCode { get; set; } = string.Empty;

        /// <summary>
        /// menu type
        /// </summary>
        public string MenuType { get; set; } = string.Empty;

        /// <summary>
        /// menu name
        /// </summary>
        public string MenuName { get; set; } = string.Empty;

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
        /// Gets or sets a value indicating whether /[use default idle time].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [use default idle time]; otherwise, <c>false</c>.
        /// </value>
        public bool UseDefaultIdleTime { get; set; } = true;

        /// <summary>
        /// Gets or sets the customized idle time.
        /// </summary>
        /// <value>
        /// The customized idle time.
        /// </value>
        public int? CustomizedIdleTime { get; set; }

        /// <summary>
        /// Gets or sets the use default refresh time.
        /// </summary>
        /// <value>
        /// The use default refresh time.
        /// </value>
        public bool? UseDefaultRefreshTime { get; set; } = false;

        /// <summary>
        /// Gets or sets the customized refresh time.
        /// </summary>
        /// <value>
        /// The customized refresh time.
        /// </value>
        public int? CustomizedRefreshTime { get; set; }

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