using Megaphone.Crawler.Core.Commands;
using Megaphone.Crawler.Core.Models;
using Megaphone.Crawler.Core.Services;
using Megaphone.Standard.Extensions;
using Megaphone.Standard.Queries;
using System.Net.Http;
using System.Net.Mime;
using System.Threading.Tasks;

namespace Megaphone.Crawler.Core.Queires
{
    internal class GetResourceFromUri : IQuery<string, Resource>
    {
        private readonly IRestService service;

        public GetResourceFromUri(IRestService client)
        {
            this.service = client;
        }

        public async Task<Resource> ExecuteAsync(string model)
        {
            var response = await service.GetAsync(model);

            return await MakeResourceAsync(response);
        }

        private async Task<Resource> MakeResourceAsync(HttpResponseMessage response)
        {
            var resourceId = response.RequestMessage.RequestUri.ToGuid().ToString();

            var resource = new Resource
            {
                Id = resourceId,
                Self = response.RequestMessage.RequestUri.ToString(),
                IsActive = response.IsSuccessStatusCode,
                StatusCode = (int)response.StatusCode
            };

            await LoadResourceDetails(response, resource);

            return resource;
        }

        private static async Task LoadResourceDetails(HttpResponseMessage response, Resource resource)
        {
            var content = await response.Content.ReadAsStringAsync();
            resource.Cache = content;

            switch (response.Content.Headers.ContentType.MediaType)
            {
                case MediaTypeNames.Text.Html:
                    {
                        var loadPageDetails = new LoadPageDetails(content);
                        await loadPageDetails.ApplyAsync(resource);
                    }
                    break;
                case "application/rss+xml":
                case "application/xml":
                case MediaTypeNames.Text.Xml:
                    {
                        var loadFeedDetails = new LoadFeedDetails(content);
                        await loadFeedDetails.ApplyAsync(resource);
                    }
                    break;
            }
        }
    }
}
