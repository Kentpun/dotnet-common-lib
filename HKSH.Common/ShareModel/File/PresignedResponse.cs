namespace HKSH.Common.ShareModel.File
{
    /// <summary>
    /// Presigned Response
    /// </summary>
    public class PresignedResponse
    {
        /// <summary>
        /// 文件可访问路径
        /// </summary>
        /// <value>
        /// The presigned URL.
        /// </value>
        public string PresignedUrl { get; set; } = string.Empty;

        /// <summary>
        /// 消息
        /// </summary>
        /// <value>
        /// The message.
        /// </value>
        public string Message { get; set; } = string.Empty;
    }
}