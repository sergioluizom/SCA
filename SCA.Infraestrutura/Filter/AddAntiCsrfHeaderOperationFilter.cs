using SCA.Infraestrutura.Middleware;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace SCA.Infraestrutura.Filter
{
    public class AddAntiCsrfHeaderOperationFilter : IOperationFilter
    {
        public void Apply(Operation operation, OperationFilterContext context)
        {
            operation.Parameters?.Add(new NonBodyParameter
            {
                Name = AntiCSRFMiddleware.AntiCsrfKey,
                Description = "AntiCSRF Token",
                Required = false,
                Type = "string",
                @In = "header",
            });
        }
    }
}