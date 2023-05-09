namespace HKSH.Common.AuditLogs;

/// <summary>
/// AuditLogStoreDatabaseSettings
/// </summary>
public class AuditLogStoreDatabaseSettings
{
    /// <summary>
    /// The section
    /// </summary>
    public const string Section = "AuditLogStoreDatabase";

    /// <summary>
    /// Gets or sets the connection string.
    /// </summary>
    /// <value>
    /// The connection string.
    /// </value>
    public string ConnectionString { get; set; } = null!;

    /// <summary>
    /// Gets or sets the name of the database.
    /// </summary>
    /// <value>
    /// The name of the database.
    /// </value>
    public string DatabaseName { get; set; } = null!;

    /// <summary>
    /// Gets or sets the name of the audit logs collection.
    /// </summary>
    /// <value>
    /// The name of the audit logs collection.
    /// </value>
    public string AuditLogsCollectionName { get; set; } = null!;
}