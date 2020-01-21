using Microsoft.Extensions.Configuration;

namespace SCA.Utils.Http
{
    public class UriBuilder
    {
        private static IConfiguration configuration;
        public static string GetURI(string key)
        {
            return configuration[key];
        }
    }
}
