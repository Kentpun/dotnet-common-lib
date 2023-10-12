namespace HKSH.Common.ShareModel.ExportImport
{
    /// <summary>
    /// TaskResponse
    /// </summary>
    public class TaskResponse
    {
        /// <summary>
        /// Gets or sets the task code.
        /// </summary>
        /// <value>
        /// The task code.
        /// </value>
        public string? TaskCode { get; set; }

        /// <summary>
        /// Gets or sets the type of the acm.
        /// Source HKSH.Common/Enums/TaskType
        /// </summary>
        /// <value>
        /// The type of the acm.
        /// </value>
        public string TaskType { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the action.
        /// Source HKSH.Common/Enums/ExportImportType
        /// </summary>
        /// <value>
        /// The action.
        /// </value>
        public string Action { get; set; } = string.Empty;

        /// <summary>
        /// 文件名
        /// </summary>
        /// <value>
        /// The name of the file.
        /// </value>
        public string FileName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the name of the bucket.
        /// </summary>
        /// <value>
        /// The name of the bucket.
        /// </value>
        public string BucketName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the name of the object.
        /// </summary>
        /// <value>
        /// The name of the object.
        /// </value>
        public string ObjectName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the status.
        /// Source HKSH.Common/Enums/TaskStatus
        /// </summary>
        /// <value>
        /// The status.
        /// </value>
        public string Status { get; set; } = string.Empty;

        /// <summary>
        /// 狀態變更原因
        /// </summary>
        /// <value>
        /// The remark.
        /// </value>
        public string Remark { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the created time.
        /// </summary>
        /// <value>
        /// The created time.
        /// </value>
        public DateTime CreatedTime { get; set; }
    }
}