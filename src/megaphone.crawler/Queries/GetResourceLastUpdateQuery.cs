using Dapr.Client;
using Megaphone.Crawler.Representations;
using Megaphone.Standard.Extensions;
using Megaphone.Standard.Queries;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Megaphone.Crawler.Queries
{
    internal class GetResourceLastUpdateQuery : IQuery<DaprClient, ResourceLastUpdateRepresentation>
    {
        private readonly Guid id;
        private readonly string host;

        public GetResourceLastUpdateQuery(string url)
        {
            var uri = url.ToUri();
            id = uri.ToGuid();
            host = uri.Host;
        }

        public async Task<ResourceLastUpdateRepresentation> ExecuteAsync(DaprClient model)
        {
            var response = await model.InvokeMethodAsync<ResourceLastUpdateRepresentation>(HttpMethod.Get, "resources", $"api/resources/{host}/{id}/last-updated");
            return response;
        }
    }
}
