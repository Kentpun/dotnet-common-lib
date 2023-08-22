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
        /// Minio测试保留至日期
        /// </summary>
        /// <value>
        /// The retain until date.
        /// </value>
        public DateTime? RetainUntilDate { get; set; }
    }
}