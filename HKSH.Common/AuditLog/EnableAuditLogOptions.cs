namespace HKSH.Common.AuditLog;

/// <summary>
/// Enable AuditLog Options
/// </summary>
public class EnableAuditLogOptions
{
    /// <summary>
    /// The section
    /// </summary>
    public const string SECTION = "EnableAuditLogOptions";

    /// <summary>
    /// Gets or sets a value indicating whether this instance is enabled.
    /// </summary>
    /// <value>
    ///   <c>true</c> if this instance is enabled; otherwise, <c>false</c>.
    /// </value>
    public bool IsEnabled { get; set; } = false;
}