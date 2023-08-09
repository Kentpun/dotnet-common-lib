namespace HKSH.Common.ShareModel.Mdm
{
    /// <summary>
    /// DictMasterResponse
    /// </summary>
    public class DictMasterResponse
    {
        /// <summary>
        /// Gets or sets the dictionary setting code.
        /// </summary>
        /// <value>
        /// The dictionary setting code.
        /// </value>
        public string DictSettingCode { get; set; } = null!;

        /// <summary>
        /// Gets or sets the name of the dictionary setting.
        /// </summary>
        /// <value>
        /// The name of the dictionary setting.
        /// </value>
        public string DictSettingName { get; set; } = null!;

        /// <summary>
        /// Gets or sets a value indicating whether this instance is active.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is active; otherwise, <c>false</c>.
        /// </value>
        public bool IsActive { get; set; } = true;

        /// <summary>
        /// Gets or sets the data json.
        /// </summary>
        /// <value>
        /// The data json.
        /// </value>
        public string? DataJson { get; set; }
    }
}