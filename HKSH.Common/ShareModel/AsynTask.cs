using HKSH.Common.Base;
using HKSH.Common.Enums;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HKSH.Common.ShareModel
{
    /// <summary>
    /// 数据导入异步任务
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
        [Column("task_code")]
        [Comment("task_code")]
        [MaxLength(200)]
        public string? TaskCode { get; set; }

        /// <summary>
        /// Gets or sets the type of the acm.
        /// </summary>
        /// <value>
        /// The type of the acm.
        /// </value>
        [Column("task_type")]
        [Comment("task_type")]
        public TaskType TaskType { get; set; }

        /// <summary>
        /// Gets or sets the action.
        /// </summary>
        /// <value>
        /// The action.
        /// </value>
        [Column("action")]
        [Comment("action")]
        public ExportImportType Action { get; set; }

        /// <summary>
        /// 文件名
        /// </summary>
        /// <value>
        /// The name of the file.
        /// </value>
        [Column("file_name")]
        [Comment("file name")]
        [MaxLength(200)]
        public string FileName { get; set; } = string.Empty;

        ///// <summary>
        ///// 文件路径
        ///// </summary>
        ///// <value>
        ///// The file URL.
        ///// </value>
        //[Column("file_url")]
        //[Comment("file url")]
        //[MaxLength(500)]
        //public string FileUrl { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the name of the bucket.
        /// </summary>
        /// <value>
        /// The name of the bucket.
        /// </value>
        [Column("bucket_name")]
        [Comment("bucket name")]
        [MaxLength(500)]
        public string BucketName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the name of the object.
        /// </summary>
        /// <value>
        /// The name of the object.
        /// </value>
        [Column("object_name")]
        [Comment("object name")]
        [MaxLength(1000)]
        public string ObjectName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>
        /// The status.
        /// </value>
        [Column("status")]
        [Comment("status")]
        public Enums.TaskStatus Status { get; set; }

        /// <summary>
        /// 状态变更原因
        /// </summary>
        /// <value>
        /// The remark.
        /// </value>
        [Column("remark")]
        [Comment("remark")]
        [MaxLength(200)]
        public string Remark { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the query code.
        /// </summary>
        /// <value>
        /// The query code.
        /// </value>
        [Column("query_code")]
        [Comment("query_code")]
        [MaxLength(200)]
        public string? QueryCode { get; set; } = string.Empty;
    }
}