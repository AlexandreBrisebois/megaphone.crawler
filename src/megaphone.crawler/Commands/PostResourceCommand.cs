using Dapr.Client;
using Megaphone.Crawler.Core.Models;
using Megaphone.Standard.Commands;
using System.Net.Http;
using System.Threading.Tasks;

namespace Megaphone.Crawler.Commands
{
    internal class PostResourceCommand : ICommand<DaprClient>
    {
        private readonly Resource resource;

        public PostResourceCommand(Resource resource)
        {
            this.resource = resource;
        }

        public async Task ApplyAsync(DaprClient model)
        {
            await model.InvokeMethodAsync(HttpMethod.Post, "resources", "api/resources", resource);
        }
    }
}
