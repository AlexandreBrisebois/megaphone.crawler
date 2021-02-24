using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Megaphone.Crawler.Services
{

    public class ResourcePushService : IPushService
    {
        private HttpClient httpClient;

        public ResourcePushService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<HttpStatusCode> PushAsync(string url, object content)
        {
            var json = JsonSerializer.Serialize(content);
            var stringContent = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync(url, stringContent);

            return response.StatusCode;
        }
    }
}
