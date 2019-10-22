using SCA.Job.Configuration;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace SCA.Job.Service
{
    public class UserService : IUserService
    {
        //private static ILogger _logger;
        public UserService()
        {
            SetUpNLog();
        }

        public async Task AddUsers()
        {
            try
            {
                Uri uri = new Uri($"http://localhost/SCA.API/User/Add/");
                HttpClient httpClient = new HttpClient();
                HttpRequestMessage httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);
                httpRequestMessage.Headers.Add("__ANTI_CSRF_LOGIN__", "1");
                httpRequestMessage.Headers.Add("SCA_API", "8PWXRe3djyDa7ttHfG-vQm8epNtyoRMJFEwvK");
                var result = await httpClient.SendAsync(httpRequestMessage);
                if (result.IsSuccessStatusCode)
                {

                }
                else
                {

                }
            }
            catch (Exception ex)
            {
                //_logger.Error($"{DateTime.Now}: Exception is occured at PerformService(): {ex.Message}");
                throw new CustomConfigurationException(ex.Message);
            }
        }

        private void SetUpNLog()
        {

        }
    }
}