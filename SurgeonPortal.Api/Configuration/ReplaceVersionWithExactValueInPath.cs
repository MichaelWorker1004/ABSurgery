using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace SurgeonPortal.Api.Configuration
{
    public class ReplaceVersionWithExactValueInPath : IDocumentFilter
    {
        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            var paths = new OpenApiPaths();
            foreach (var item in swaggerDoc.Paths)
            {
                paths.Add(item.Key.Replace("v{version}", swaggerDoc.Info.Version), item.Value);
            }
            swaggerDoc.Paths = paths;
        }
    }
}
