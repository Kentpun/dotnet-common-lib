namespace HKSH.Common.Auditing.Attributes
{
    [AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
    public class ExcludeAuditingAttribute : Attribute
    {
    }
}