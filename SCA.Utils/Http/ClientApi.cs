using Microsoft.Extensions.Configuration;
using SCA.Infraestrutura.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace SCA.Utils.Http
{
    public class ClientApi
    {
        public static async Task<HttpResponseMessage> GetApiAsync(string servico, string querystring, IConfiguration configuration, IAntiCSRFService lib)
        {
            var section = configuration.GetSection("ClienteAPI").GetSection(servico);
            return await SendMessageAsync(lib, section, "GET", queryString: querystring);
        }

        public static async Task<HttpResponseMessage> GetApiAsync(string servico, IConfiguration configuration, IAntiCSRFService lib)
        {
            var section = configuration.GetSection("ClienteAPI").GetSection(servico);
            return await SendMessageAsync(lib, section, "GET");
        }

        public static async Task<HttpResponseMessage> PutApiAsync(string servico, string JsonPostBody, IConfiguration configuration, IAntiCSRFService lib, string querystring = null)
        {
            var section = configuration.GetSection("ClienteAPI").GetSection(servico);
            using (var content = new StringContent(JsonPostBody, Encoding.UTF8, "application/json"))
            {
                return await SendMessageAsync(lib, section, "PUT", content, queryString: querystring);
            }
        }

        public static async Task<HttpResponseMessage> PostApiAsync(string servico, string JsonPostBody, IConfiguration configuration, IAntiCSRFService lib, string querystring = null)
        {
            var section = configuration.GetSection("ClienteAPI").GetSection(servico);
            using (var content = new StringContent(JsonPostBody, Encoding.UTF8, "application/json"))
            {
                return await SendMessageAsync(lib, section, "POST", content, querystring);
            }
        }

        public static async Task<HttpResponseMessage> DeleteApiAsync(string servico, string querystring, IConfiguration configuration, IAntiCSRFService lib)
        {
            var section = configuration.GetSection("ClienteAPI").GetSection(servico);
            return await SendMessageAsync(lib, section, "DELETE", queryString: querystring);
        }

        private static async Task<HttpResponseMessage> SendMessageAsync(IAntiCSRFService lib, IConfigurationSection section, string method, HttpContent content = null, string queryString = null)
        {
            var urlBuilder = new StringBuilder(section["BaseAddress"]?.TrimEnd('/') ?? "")
               .Append(section["url"]);

            if (!string.IsNullOrWhiteSpace(queryString))
                urlBuilder.Append(queryString);

            using (var message = new HttpRequestMessage
            {
                Method = new HttpMethod(method),
                RequestUri = new Uri(urlBuilder.ToString(), UriKind.RelativeOrAbsolute)
            })
            {
                if (!(content is null))
                {
                    message.Content = content;
                    content.Headers.TryGetValues("Content-Type", out IEnumerable<string> values);

                    if (values.Any())
                        message.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(values.FirstOrDefault().Split(';')[0].Trim()));
                }

                if (!(lib is null))
                    message.Headers.TryAddWithoutValidation(lib.HeaderAntiCSRF, lib.Login);

                message.Headers.TryAddWithoutValidation(section["KeyId"], section["keyValue"]);

                return await SendAsync(message);
            }
        }

        private static async Task<HttpResponseMessage> SendAsync(HttpRequestMessage message)
        {
            using (var handler = new HttpClientHandler()
            {
                ServerCertificateCustomValidationCallback = (a, b, c, d) => true,
                SslProtocols = System.Security.Authentication.SslProtocols.Tls12,
                ClientCertificateOptions = ClientCertificateOption.Automatic,
            })
            {
                using (var client = new HttpClient(handler))
                {
                    return await client.SendAsync(message, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);
                }
            }
        }
    }
}
