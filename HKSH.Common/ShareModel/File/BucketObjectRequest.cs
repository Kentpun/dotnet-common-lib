using HKSH.Common.Enums;

namespace HKSH.Common.ShareModel.File
{
    /// <summary>
    /// Bucket Object Request
    /// </summary>
    public class BucketObjectRequest
    {
        /// <summary>
        /// 存儲桶名稱
        /// </summary>
        /// <value>
        /// The name of the bucket.
        /// </value>
        public string BucketName { get; set; } = string.Empty;

        /// <summary>
        /// 對象名稱
        /// </summary>
        /// <value>
        /// The name of the object.
        /// </value>
        public string ObjectName { get; set; } = string.Empty;

        /// <summary>
        /// 過期時間
        /// </summary>
        /// <value>
        /// The expire in.
        /// </value>
        public int? ExpireIn { get; set; }

        /// <summary>
        /// 過期時間單位
        /// </summary>
        /// <value>
        /// The expiry time unit.
        /// </value>
        public TimeUnit ExpiryTimeUnit { get; set; } = TimeUnit.Minute;
    }
}