namespace HKSH.Common.File
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

    /// <summary>
    /// StreamRequest
    /// </summary>
    public class StreamRequest
    {
        /// <summary>
        /// Gets or sets the stream.
        /// </summary>
        /// <value>
        /// The stream.
        /// </value>
        public Stream Stream 
        {
            get 
            {
                return new MemoryStream(Bytes);
            }
        }

        /// <summary>
        /// Gets or sets the bytes.
        /// </summary>
        /// <value>
        /// The bytes.
        /// </value>
        public byte[] Bytes { get; set; } = null!;

        /// <summary>
        /// Gets or sets the name of the file.
        /// </summary>
        /// <value>
        /// The name of the file.
        /// </value>
        public string FileName { get; set; } = null!;
    }
}
