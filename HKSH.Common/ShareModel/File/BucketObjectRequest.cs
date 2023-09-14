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
    }
}