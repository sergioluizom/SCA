using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace SCA.Infraestrutura.Middleware
{
    public class KeyIdMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration _configuration;

        public KeyIdMiddleware(RequestDelegate next, IConfiguration configuration)
        {
            _next = next;
            _configuration = configuration;
        }

        public async Task Invoke(HttpContext context)
        {
            var key = _configuration["keyName"];
            var value = _configuration["keyValue"];

            if (string.IsNullOrWhiteSpace(key) || string.IsNullOrWhiteSpace(value))
            {
                await UnauthorizeResponse(context, 1, "APIKey não configurado, contate o adminstrador do sistema");
                return;
            }

            var headerValue = context.Request.Headers[key];
            if (string.IsNullOrWhiteSpace(headerValue))
            {
                await UnauthorizeResponse(context, 1, "APIKey não encontrado");
                return;
            }

            if (!string.Equals(value, headerValue, StringComparison.InvariantCultureIgnoreCase))
            {
                await UnauthorizeResponse(context, 1, "APIKey inválido");
                return;
            }

            await _next.Invoke(context);
        }

        private static async Task UnauthorizeResponse(HttpContext context, int codigo, string descricao)
        {
            context.Response.StatusCode = 401;
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(JsonConvert.SerializeObject(new { codigo, descricao }));
        }
    }
}
