using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace HKSH.Common.AuditLogs.Models
{
    public class RowAuditLog
    {
        /// <summary>
        /// MongoDB 主键.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        /// <summary>
        /// Gets or sets the operator.
        /// </summary>
        /// <value>
        /// The operator.
        /// </value>
        [BsonElement("Action")]
        public string Action { get; set; } = null!;

        /// <summary>
        /// 行记录所属表.
        /// </summary>
        /// <value>
        /// The name of the table.
        /// </value>
        [BsonElement("TableName")]
        public string TableName { get; set; } = null!;

        /// <summary>
        /// 行记录Id.
        /// </summary>
        /// <value>
        /// The type of the business.
        /// </value>
        [BsonElement("RowId")]
        public string? RowId { get; set; }

        /// <summary>
        /// 业务编号(类似于组装后的FormId(ALC.434.001)).
        /// </summary>
        /// <value>
        /// The type of the business.
        /// </value>
        [BsonElement("BusinessCode")]
        public string? BusinessCode { get; set; } = null!;

        /// <summary>
        /// 版本 时间戳.
        /// </summary>
        /// <value>
        /// The log message.
        /// </value>
        [BsonElement("Version")]
        public string Version { get; set; } = null!;

        /// <summary>
        /// 操作人.
        /// </summary>
        /// <value>
        /// The update by.
        /// </value>
        [BsonElement("UpdateBy")]
        public string? UpdateBy { get; set; } = null!;

        /// <summary>
        /// 行记录Json结构.
        /// </summary>
        /// <value>
        /// The row.
        /// </value>
        [BsonElement("Row")]
        public string Row { get; set; } = null!;
    }
}