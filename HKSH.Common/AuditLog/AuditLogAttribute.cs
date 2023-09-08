namespace HKSH.Common.AuditLog;

/// <summary>
/// AuditLog Attribute
/// </summary>
/// <seealso cref="Attribute" />
[AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
public class AuditLogAttribute : Attribute
{
}