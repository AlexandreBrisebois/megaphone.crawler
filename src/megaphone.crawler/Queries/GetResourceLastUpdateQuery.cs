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

        public GetResourceLastUpdateQuery(string url)
        {
            id = url.ToUri().ToGuid();
        }

        public async Task<ResourceLastUpdateRepresentation> ExecuteAsync(DaprClient model)
        {
            var response = await model.InvokeMethodAsync<ResourceLastUpdateRepresentation>(HttpMethod.Get, "resources", $"api/resources/{id}/last-updated");
            return response;
        }
    }
}
