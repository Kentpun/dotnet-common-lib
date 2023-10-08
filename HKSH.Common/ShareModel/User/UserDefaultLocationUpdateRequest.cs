namespace HKSH.Common.ShareModel.User
{
    /// <summary>
    /// User Default Location Update Request
    /// </summary>
    public class UserDefaultLocationUpdateRequest
    {
        /// <summary>
        /// Gets or sets the module.
        /// </summary>
        /// <value>
        /// The module.
        /// </value>
        public string Module { get; set; } = null!;

        /// <summary>
        /// Gets or sets the location code.
        /// </summary>
        /// <value>
        /// The location code.
        /// </value>
        public string LocationCode { get; set; } = string.Empty;
    }
}
