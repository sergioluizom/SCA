using Microsoft.OpenApi.Models;
using SCA.Infraestrutura.Middleware;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace SCA.Infraestrutura.Filter
{
    public class AddAntiCsrfHeaderOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            operation.Parameters?.Add(new OpenApiParameter
            {
                Name = AntiCSRFMiddleware.AntiCsrfKey,
                Description = "AntiCSRF Token",
                Required = false,                
                @In = ParameterLocation.Header,
            });
        }
    }
}