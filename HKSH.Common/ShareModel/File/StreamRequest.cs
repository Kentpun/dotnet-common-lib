namespace HKSH.Common.ShareModel.File
{
    /// <summary>
    /// StreamRequest
    /// </summary>
    public class StreamRequest
    {
        /// <summary>
        /// Gets or sets the bytes.
        /// </summary>
        /// <value>
        /// The bytes.
        /// </value>
        public string Json { get; set; } = null!;

        /// <summary>
        /// Gets or sets the name of the file.
        /// </summary>
        /// <value>
        /// The name of the file.
        /// </value>
        public string FileName { get; set; } = null!;
    }
}
