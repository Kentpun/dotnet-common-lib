﻿namespace HKSH.Common.ShareModel.File
{
    /// <summary>
    /// Bucket Object Request
    /// </summary>
    public class BucketObjectRequest
    {
        /// <summary>
        /// 存储桶名称
        /// </summary>
        /// <value>
        /// The name of the bucket.
        /// </value>
        public string BucketName { get; set; } = string.Empty;

        /// <summary>
        /// 对象名称
        /// </summary>
        /// <value>
        /// The name of the object.
        /// </value>
        public string ObjectName { get; set; } = string.Empty;
    }
}