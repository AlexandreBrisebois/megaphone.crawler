using Dapr.Client;
using Megaphone.Crawler.Core.Models;
using Megaphone.Standard.Commands;
using Megaphone.Standard.Messages;
using System.Threading.Tasks;

namespace Megaphone.Crawler.Commands
{

    internal class SendCrawlRequestCommand : ICommand<DaprClient>
    {
        private readonly CommandMessage message;

        public SendCrawlRequestCommand(string uri)
        {
            message = MessageBuilder.NewCommand("crawl-request")
                                        .WithParameters("uri", uri)
                                        .Make();
        }

        public SendCrawlRequestCommand(Resource resource)
        {
            message = MessageBuilder.NewCommand("crawl-request")
                                        .WithParameters("uri", resource.Self)
                                        .WithParameters("display", resource.Display)
                                        .WithParameters("description", resource.Description)
                                        .WithParameters("published", resource.Published.ToString())
                                        .Make();
        }

        public async Task ApplyAsync(DaprClient model)
        {
            await model.InvokeBindingAsync("crawl-requests", "create", message);
        }
    }
}
