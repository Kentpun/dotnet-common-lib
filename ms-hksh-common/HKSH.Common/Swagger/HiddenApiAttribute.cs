namespace HKSH.Common.Swagger
{
    /// <summary>
    /// HiddenApiAttribute
    /// </summary>
    /// <seealso cref="Attribute" />
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
    internal class HiddenApiAttribute : Attribute
    {
    }
}