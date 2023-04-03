using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;

namespace HKSH.Common.Swagger
{
    /// <summary>
    /// HiddenApiFilter
    /// </summary>
    /// <seealso cref="IDocumentFilter" />
    public class HiddenApiFilter : IDocumentFilter
    {
        /// <summary>
        /// Applies the specified swagger document.
        /// </summary>
        /// <param name="swaggerDoc">The swagger document.</param>
        /// <param name="context">The context.</param>
        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            foreach (ApiDescription apiDescription in context.ApiDescriptions)
            {
                if (apiDescription.TryGetMethodInfo(out MethodInfo method))
                {
                    if (method.ReflectedType != null && method.ReflectedType.CustomAttributes.Any(t => t.AttributeType == typeof(HiddenApiAttribute))
                            || method.CustomAttributes.Any(t => t.AttributeType == typeof(HiddenApiAttribute)))
                    {
                        string key = "/" + apiDescription.RelativePath;
                        if (key.Contains('?'))
                        {
                            int idx = key.IndexOf("?", StringComparison.Ordinal);
                            key = key[..idx];
                        }
                        swaggerDoc.Paths.Remove(key);
                    }
                }
            }
        }
    }
}