using Dapr.Client;
using Megaphone.Crawler.Representations;
using Megaphone.Standard.Extensions;
using Megaphone.Standard.Queries;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Megaphone.Crawler.Queries
{
    internal class GetResourceLastUpdateQuery : IQuery<DaprClient, DateTimeOffset>
    {
        private readonly Guid id;

        public GetResourceLastUpdateQuery(string url)
        {
            id = url.ToUri().ToGuid();
        }

        public async Task<DateTimeOffset> ExecuteAsync(DaprClient model)
        {
            var response = await model.InvokeMethodAsync<ResourceLastUpdateRepresentation>(HttpMethod.Head, "resources", $"api/resources/{id}");
            return response.LastUpdated;
        }
    }
}
