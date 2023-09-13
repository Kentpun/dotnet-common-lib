namespace HKSH.Common.ShareModel.Base
{
    /// <summary>
    /// IEntityTracker
    /// </summary>
    public interface IEntityTracker
    {
        /// <summary>
        /// Gets or sets the created time.
        /// </summary>
        /// <value>
        /// The created time.
        /// </value>
        public DateTime CreatedTime { get; set; }

        /// <summary>
        /// Gets or sets the created by.
        /// </summary>
        /// <value>
        /// The created by.
        /// </value>
        public string CreatedBy { get; set; }

        /// <summary>
        /// Gets or sets the modified time.
        /// </summary>
        /// <value>
        /// The modified time.
        /// </value>
        public DateTime? ModifiedTime { get; set; }

        /// <summary>
        /// Gets or sets the modified by.
        /// </summary>
        /// <value>
        /// The modified by.
        /// </value>
        public string? ModifiedBy { get; set; }

        /// <summary>
        /// System Trace ID
        /// </summary>
        /// <value>
        /// The trace identifier.
        /// </value>
        public string? TraceId { get; set; }
    }
}