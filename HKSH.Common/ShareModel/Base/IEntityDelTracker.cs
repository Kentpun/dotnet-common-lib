namespace HKSH.Common.ShareModel.Base
{
    /// <summary>
    /// IEntityDelTracker
    /// </summary>
    public interface IEntityDelTracker
    {
        /// <summary>
        /// Gets or sets a value indicating whether this instance is deleted.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is deleted; otherwise, <c>false</c>.
        /// </value>
        public byte RecordStatus { get; set; }

        /// <summary>
        /// Gets or sets the deleted time.
        /// </summary>
        /// <value>
        /// The deleted time.
        /// </value>
        public DateTime? DeletedTime { get; set; }

        /// <summary>
        /// Gets or sets the deleted by.
        /// </summary>
        /// <value>
        /// The deleted by.
        /// </value>
        public string? DeletedBy { get; set; }
    }
}