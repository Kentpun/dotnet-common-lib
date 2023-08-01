namespace HKSH.Common.File
{
    /// <summary>
    /// DownloadFileRequest
    /// </summary>
    public class DownloadFileRequest
    {
        /// <summary>
        /// Gets or sets the key.
        /// </summary>
        /// <value>
        /// The key.
        /// </value>
        public string Key { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the name of the file.
        /// </summary>
        /// <value>
        /// The name of the file.
        /// </value>
        public string FileName { get; set; } = null!;
    }
}