using HKSH.Common.Enums;

namespace HKSH.Common.ShareModel.File
{
    /// <summary>
    /// UploadFileRequest
    /// </summary>
    public class UploadFileRequest
    {
        /// <summary>
        /// 文件上传Key
        /// </summary>
        /// <value>
        /// The key.
        /// </value>
        public string Key { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the expire in.
        /// </summary>
        /// <value>
        /// The expire in.
        /// </value>
        public int? ExpireIn { get; set; }

        /// <summary>
        /// Gets or sets the expiry time unit.
        /// </summary>
        /// <value>
        /// The expiry time unit.
        /// </value>
        public TimeUnit ExpiryTimeUnit { get; set; } = TimeUnit.Minute;
    }
}