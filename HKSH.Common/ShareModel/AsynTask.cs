using HKSH.Common.ShareModel.Base;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace HKSH.Common.ShareModel
{
    /// <summary>
    /// 數據導入異步任務
    /// </summary>
    /// <seealso cref="BaseTrackedEntity" />
    public class AsynTask : BaseTrackedEntity
    {
        /// <summary>
        /// Gets or sets the task code.
        /// </summary>
        /// <value>
        /// The task code.
        /// </value>
        [Comment("Task Code")]
        [MaxLength(200)]
        public string? TaskCode { get; set; }

        /// <summary>
        /// Gets or sets the type of the acm.
        /// Source HKSH.Common/Enums/TaskType
        /// </summary>
        /// <value>
        /// The type of the acm.
        /// </value>
        [Comment("Task Type")]
        [MaxLength(100)]
        public string TaskType { get; set; } = null!;

        /// <summary>
        /// Gets or sets the action.
        /// Source HKSH.Common/Enums/ExportImportType
        /// </summary>
        /// <value>
        /// The action.
        /// </value>
        [Comment("Action")]
        [MaxLength(100)]
        public string Action { get; set; } = null!;

        /// <summary>
        /// 文件名
        /// </summary>
        /// <value>
        /// The name of the file.
        /// </value>
        [Comment("File Name")]
        [MaxLength(200)]
        public string FileName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the name of the bucket.
        /// </summary>
        /// <value>
        /// The name of the bucket.
        /// </value>
        [Comment("Bucket Name")]
        [MaxLength(500)]
        public string BucketName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the name of the object.
        /// </summary>
        /// <value>
        /// The name of the object.
        /// </value>
        [Comment("Object Name")]
        [MaxLength(1000)]
        public string ObjectName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the status.
        /// Source HKSH.Common/Enums/TaskStatus
        /// </summary>
        /// <value>
        /// The status.
        /// </value>
        [Comment("Status")]
        [MaxLength(100)]
        public string Status { get; set; } = null!;

        /// <summary>
        /// 狀態變更原因
        /// </summary>
        /// <value>
        /// The remark.
        /// </value>
        [Comment("Remark")]
        [MaxLength(500)]
        public string Remark { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the query code.
        /// </summary>
        /// <value>
        /// The query code.
        /// </value>
        [Comment("Query Code")]
        [MaxLength(200)]
        public string? QueryCode { get; set; } = string.Empty;
    }
}