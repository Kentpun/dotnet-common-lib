using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace HKSH.Common.AuditLogs.Models
{
    public class FieldAuditLog
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        /// <summary>
        /// 类似于组装后的FormId(ALC.434.001).
        /// </summary>
        /// <value>
        /// The section.
        /// </value>
        [BsonElement("Section")]
        public string Section { get; set; } = null!;

        /// <summary>
        /// 修改字段.
        /// </summary>
        /// <value>
        /// The field.
        /// </value>
        [BsonElement("Field")]
        public string Field { get; set; } = null!;

        /// <summary>
        /// 原始值.
        /// </summary>
        /// <value>
        /// The update from.
        /// </value>
        [BsonElement("UpdateFrom")]
        public string UpdateFrom { get; set; } = null!;

        /// <summary>
        /// 修改值.
        /// </summary>
        /// <value>
        /// The update to.
        /// </value>
        [BsonElement("UpdateTo")]
        public string UpdateTo { get; set; } = null!;

        /// <summary>
        /// 修改的时间.
        /// </summary>
        /// <value>
        /// The update time.
        /// </value>
        [BsonElement("UpdateTime")]
        public DateTime UpdateTime { get; set; }

        /// <summary>
        /// 修改的操作人.
        /// </summary>
        /// <value>
        /// The update by.
        /// </value>
        [BsonElement("UpdateBy")]
        public string UpdateBy { get; set; } = null!;

        /// <summary>
        /// Gets or sets the operator.
        /// </summary>
        /// <value>
        /// The operator.
        /// </value>
        [BsonElement("Action")]
        public string Action { get; set; } = null!;
    }
}