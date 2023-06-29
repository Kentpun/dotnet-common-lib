namespace HKSH.Common.ShareModel
{
    /// <summary>
    /// Dictionary
    /// </summary>
    public class Dictionary
    {
        /// <summary>
        /// Gets or sets the data set code.
        /// </summary>
        /// <value>
        /// The data set code.
        /// </value>
        public string DataSetCode { get; set; } = null!;

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; } = null!;

        /// <summary>
        /// Gets or sets the code.
        /// </summary>
        /// <value>
        /// The code.
        /// </value>
        public string Code { get; set; } = null!;

        /// <summary>
        /// Gets or sets the parent code.
        /// </summary>
        /// <value>
        /// The parent code.
        /// </value>
        public string? ParentCode { get; set; }

        /// <summary>
        /// Gets or sets the order no.
        /// </summary>
        /// <value>
        /// The order no.
        /// </value>
        public int OrderNo { get; set; }
    }
}