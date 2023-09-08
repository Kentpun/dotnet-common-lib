namespace HKSH.Common.ShareModel.AduitLog
{
    /// <summary>
    /// PrintLogDocument
    /// </summary>
    public class PrintLogDocument : BaseLogModel
    {
        /// <summary>
        /// Gets or sets the name of the bucket.
        /// </summary>
        /// <value>
        /// The name of the bucket.
        /// </value>
        public string BucketName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the object names.
        /// </summary>
        /// <value>
        /// The object names.
        /// </value>
        public string ObjectNames { get; set; } = string.Empty;
    }
}