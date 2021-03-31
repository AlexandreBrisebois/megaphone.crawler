using Dapr.Client;
using Megaphone.Crawler.Core.Models;
using Megaphone.Crawler.Core.Services;
using Megaphone.Crawler.Representations;
using Megaphone.Standard.Extensions;
using System.Net.Http;
using System.Threading.Tasks;

namespace Megaphone.Crawler.Services
{
    public class DaprResourceService : IResourceService
    {
        private readonly DaprClient daprClient;

        public DaprResourceService(DaprClient daprClient)
        {
            this.daprClient = daprClient;
        }
        public async Task<ResourceStatus> GetStatusAsync(string url)
        {
            var uri = url.ToUri();
            var id = uri.ToGuid();
            var host = uri.Host;

            var response = await daprClient.InvokeMethodAsync<ResourceStatusRepresentation>(HttpMethod.Get, "resources", $"api/resources/{host}/{id}/status");

            return new()
            {
                IsActive = response.IsActive,
                LastUpdated = response.LastUpdated,
                Type = response.Type
            };
        }

        public async Task PostAsync(Resource resource)
        {
            await daprClient.InvokeMethodAsync(HttpMethod.Post, "resources", "api/resources", resource);
        }
    }
}
