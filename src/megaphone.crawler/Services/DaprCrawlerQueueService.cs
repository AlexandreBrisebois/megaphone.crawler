using Dapr.Client;
using Megaphone.Crawler.Core.Services;
using Megaphone.Standard.Messages;
using System.Threading.Tasks;

namespace Megaphone.Crawler.Services
{
    public class DaprCrawlerQueueService : ICrawlerQueueService
    {
        private readonly DaprClient daprClient;

        public DaprCrawlerQueueService(DaprClient daprClient)
        {
            this.daprClient = daprClient;
        }

        public async Task Enqueue(CommandMessage commandMessage)
        {
            await daprClient.InvokeBindingAsync("crawl-requests", "create", commandMessage);
        }
    }
}
