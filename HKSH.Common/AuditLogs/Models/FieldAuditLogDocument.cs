﻿namespace HKSH.Common.AuditLogs.Models
{
    public class FieldAuditLogDocument
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public string? Id { get; set; }

        /// <summary>
        /// 类似于组装后的FormId(ALC.434.001).
        /// </summary>
        /// <value>
        /// The section.
        /// </value>
        public string Section { get; set; } = null!;

        /// <summary>
        /// 修改字段.
        /// </summary>
        /// <value>
        /// The field.
        /// </value>
        public string Field { get; set; } = null!;

        /// <summary>
        /// 原始值.
        /// </summary>
        /// <value>
        /// The update from.
        /// </value>
        public string UpdateFrom { get; set; } = null!;

        /// <summary>
        /// 修改值.
        /// </summary>
        /// <value>
        /// The update to.
        /// </value>
        public string UpdateTo { get; set; } = null!;

        /// <summary>
        /// 修改的时间.
        /// </summary>
        /// <value>
        /// The update time.
        /// </value>
        public DateTime UpdateTime { get; set; }

        /// <summary>
        /// 修改的操作人.
        /// </summary>
        /// <value>
        /// The update by.
        /// </value>
        public string UpdateBy { get; set; } = null!;

        /// <summary>
        /// Gets or sets the operator.
        /// </summary>
        /// <value>
        /// The operator.
        /// </value>
        public string Action { get; set; } = null!;
    }
}