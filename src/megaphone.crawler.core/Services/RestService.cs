using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Megaphone.Crawler.Core.Services
{
    public class RestService : IRestService
    {
        private HttpClient httpClient;

        public RestService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<HttpResponseMessage> GetAsync(string url)
        {
            var response = await httpClient.GetAsync(url);
            return response;
        }

        public async Task<HttpStatusCode> PostAsync(string url, object content)
        {
            var json = JsonSerializer.Serialize(content);
            var stringContent = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync(url, stringContent);

            return response.StatusCode;
        }
    }
}
