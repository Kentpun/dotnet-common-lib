using HKSH.Common.Enums;
using TaskStatus = HKSH.Common.Enums.TaskStatus;

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
        /// </summary>
        /// <value>
        /// The type of the acm.
        /// </value>
        public TaskType TaskType { get; set; }

        /// <summary>
        /// Gets or sets the action.
        /// </summary>
        /// <value>
        /// The action.
        /// </value>
        public ExportImportType Action { get; set; }

        /// <summary>
        /// 文件名
        /// </summary>
        /// <value>
        /// The name of the file.
        /// </value>
        public string FileName { get; set; } = String.Empty;

        /// <summary>
        /// Gets or sets the name of the bucket.
        /// </summary>
        /// <value>
        /// The name of the bucket.
        /// </value>
        public string BucketName { get; set; } = String.Empty;

        /// <summary>
        /// Gets or sets the name of the object.
        /// </summary>
        /// <value>
        /// The name of the object.
        /// </value>
        public string ObjectName { get; set; } = String.Empty;

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>
        /// The status.
        /// </value>
        public TaskStatus Status { get; set; }

        /// <summary>
        /// 状态变更原因
        /// </summary>
        /// <value>
        /// The remark.
        /// </value>
        public string Remark { get; set; } = String.Empty;

        /// <summary>
        /// Gets or sets the created at.
        /// </summary>
        /// <value>
        /// The created at.
        /// </value>
        public DateTime CreatedAt { get; set; }
    }
}