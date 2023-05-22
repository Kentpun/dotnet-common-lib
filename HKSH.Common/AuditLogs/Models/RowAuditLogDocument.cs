namespace HKSH.Common.AuditLogs.Models
{
    public class RowAuditLogDocument
    {
        /// <summary>
        /// MongoDB 主键.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public string? Id { get; set; }

        /// <summary>
        /// Gets or sets the operator.
        /// </summary>
        /// <value>
        /// The operator.
        /// </value>
        public string Action { get; set; } = null!;

        /// <summary>
        /// 行记录所属表.
        /// </summary>
        /// <value>
        /// The name of the table.
        /// </value>
        public string TableName { get; set; } = null!;

        /// <summary>
        /// 行记录Id.
        /// </summary>
        /// <value>
        /// The type of the business.
        /// </value>
        public string? RowId { get; set; }

        /// <summary>
        /// 业务编号(类似于组装后的FormId(ALC.434.001)).
        /// </summary>
        /// <value>
        /// The type of the business.
        /// </value>
        public string? BusinessCode { get; set; } = null!;

        /// <summary>
        /// 版本 时间戳.
        /// </summary>
        /// <value>
        /// The log message.
        /// </value>
        public string Version { get; set; } = null!;

        /// <summary>
        /// 操作人.
        /// </summary>
        /// <value>
        /// The update by.
        /// </value>
        public string? UpdateBy { get; set; } = null!;

        /// <summary>
        /// 行记录Json结构.
        /// </summary>
        /// <value>
        /// The row.
        /// </value>
        public string Row { get; set; } = null!;
    }
}