using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace SCA.Infraestrutura.Middleware
{
    public class AntiCSRFMiddleware
    {
        private readonly RequestDelegate _next;
        public const string AntiCsrfKey = "__ANTI_CSRF_LOGIN__";

        public AntiCSRFMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var csrf = context.Request.Headers[AntiCsrfKey];

            if (string.IsNullOrEmpty(csrf) || string.IsNullOrWhiteSpace(csrf))
            {
                //Unauthorized
                context.Response.StatusCode = 401;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(JsonConvert.SerializeObject(new { codigo = 2, descricao = "Usuário não encontrado" }));
                return;
            }
            await _next.Invoke(context);
        }
    }
}