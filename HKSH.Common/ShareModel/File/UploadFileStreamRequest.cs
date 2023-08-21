namespace HKSH.Common.ShareModel.File
{
    /// <summary>
    /// UploadFileStreamRequest
    /// </summary>
    /// <seealso cref="UploadFileRequest" />
    public class UploadFileStreamRequest : UploadFileRequest
    {
        /// <summary>
        /// Gets or sets the files.
        /// </summary>
        /// <value>
        /// The files.
        /// </value>
        public List<StreamRequest> Files { get; set; } = new();
    }
}