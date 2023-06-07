namespace HKSH.Common.Repository.Database
{
    /// <summary>
    /// user
    /// </summary>
    public class ClaimCurrentUser
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public long Id { get; set; }

        /// <summary>
        /// Gets or sets the department identifier.
        /// </summary>
        /// <value>
        /// The department identifier.
        /// </value>
        public string DepartmentId { get; set; }

        /// <summary>
        /// login name
        /// </summary>
        public string LoginName { get; set; } = String.Empty;

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>
        /// The title.
        /// </value>
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the user name en.
        /// </summary>
        /// <value>
        /// The user name en.
        /// </value>
        public string UserNameEn { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the user name cn.
        /// </summary>
        /// <value>
        /// The user name cn.
        /// </value>
        public string UserNameCn { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        public string RoleType { get; set; } = string.Empty;

        /// <summary>
        /// domain login
        /// </summary>
        public string DomainLogin { get; set; } = String.Empty;
    }
}