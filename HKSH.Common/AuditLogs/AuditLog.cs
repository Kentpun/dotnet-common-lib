using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace HKSH.Common.AuditLogs;

/// <summary>
/// AuditLog
/// </summary>
public class AuditLog
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
    /// Gets or sets the type of the business.
    /// </summary>
    /// <value>
    /// The type of the business.
    /// </value>
    [BsonElement("BusinessType")]
    public string BusinessType { get; set; } = null!;

    /// <summary>
    /// Gets or sets the log message.
    /// </summary>
    /// <value>
    /// The log message.
    /// </value>
    [BsonElement("Version")]
    public string Version { get; set; } = null!;

    /// <summary>
    /// Gets or sets the operator.
    /// </summary>
    /// <value>
    /// The operator.
    /// </value>
    [BsonElement("Action")]
    public string Action { get; set; } = null!;
}