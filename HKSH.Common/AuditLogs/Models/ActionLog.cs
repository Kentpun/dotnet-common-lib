using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace HKSH.Common.AuditLogs.Models
{
    /// <summary>
    /// ActionLog
    /// </summary>
    public class ActionLog
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
        /// Gets or sets the action date.
        /// </summary>
        /// <value>
        /// The action date.
        /// </value>
        [BsonElement("ActionDate")]
        public DateTime ActionDate { get; set; }

        /// <summary>
        /// Gets or sets the user.
        /// </summary>
        /// <value>
        /// The user.
        /// </value>
        [BsonElement("User")]
        public string User { get; set; } = null!;

        /// <summary>
        /// Gets or sets the device.
        /// </summary>
        /// <value>
        /// The device.
        /// </value>
        [BsonElement("Device")]
        public string Device { get; set; } = null!;

        /// <summary>
        /// Gets or sets the function.
        /// </summary>
        /// <value>
        /// The function.
        /// </value>
        [BsonElement("Function")]
        public string Function { get; set; } = null!;

        /// <summary>
        /// Gets or sets the sub function.
        /// </summary>
        /// <value>
        /// The sub function.
        /// </value>
        [BsonElement("SubFunction")]
        public string SubFunction { get; set; } = null!;

        /// <summary>
        /// Gets or sets the patient identifier.
        /// </summary>
        /// <value>
        /// The patient identifier.
        /// </value>
        [BsonElement("PatientID")]
        public string PatientID { get; set; } = null!;

        /// <summary>
        /// Gets or sets the visti file no.
        /// </summary>
        /// <value>
        /// The visti file no.
        /// </value>
        [BsonElement("VistiFileNo")]
        public string VistiFileNo { get; set; } = null!;

        /// <summary>
        /// Gets or sets the action.
        /// </summary>
        /// <value>
        /// The action.
        /// </value>
        [BsonElement("Action")]
        public string Action { get; set; } = null!;

        /// <summary>
        /// Gets or sets the content.
        /// </summary>
        /// <value>
        /// The content.
        /// </value>
        [BsonElement("Content")]
        public string Content { get; set; } = null!;
    }
}