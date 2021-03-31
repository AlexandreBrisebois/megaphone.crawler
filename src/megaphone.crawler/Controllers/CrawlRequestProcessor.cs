using Megaphone.Crawler.Core;
using Megaphone.Crawler.Core.Models;
using Megaphone.Crawler.Core.Services;
using Megaphone.Crawler.Models;
using Megaphone.Standard.Messages;
using Megaphone.Standard.Time;
using Microsoft.ApplicationInsights;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Megaphone.Crawler.Controllers
{

    [ApiController]
    [Route("/")]
    public class CrawlRequestProcessor : ControllerBase
    {
        private readonly int interval;

        private readonly TelemetryClient telemetryClient;
        private readonly IResourceService resourceService;
        private readonly IWebResourceCrawler crawler;
        private readonly ICrawlerQueueService crawlerQueueService;
        private readonly IClock clock;

        public CrawlRequestProcessor(TelemetryClient telemetryClient,
                                     [FromServices] IResourceService resourceService,
                                     [FromServices] IWebResourceCrawler crawler,
                                     [FromServices] ICrawlerQueueService crawlerQueueService,
                                     [FromServices] IClock clock)
        {
            this.interval = Convert.ToInt32(Environment.GetEnvironmentVariable("CRAWL-INTERVAL") ?? "120");
           
            this.telemetryClient = telemetryClient;
            this.resourceService = resourceService;
            this.crawler = crawler;
            this.crawlerQueueService = crawlerQueueService;
            this.clock = clock;
        }

        private async Task<bool> ShouldSkipCrawl(string uri)
        {

            var resourceStatus = await resourceService.GetStatusAsync(uri);

            if (resourceStatus.LastUpdated == DateTimeOffset.MinValue)
                return false;

            if (resourceStatus.Type == ResourceType.Feed)
                return false;

            if (clock.Now > resourceStatus.LastUpdated.AddMinutes(this.interval))
            {
                return false;
            }

            return true;
        }

        [HttpPost("crawl-requests")]
        public async Task<IActionResult> PostAsync(CommandMessage message)
        {
            if (message.Action == "crawl-request")
            {
                string uri = message.Parameters["uri"];

                if (await ShouldSkipCrawl(uri))
                    return Ok();

                var resource = await crawler.GetResourceAsync(uri);

                SetValuesFromPatameters(message, resource);

                await this.resourceService.PostAsync(resource);

                foreach (var r in resource.Resources)
                {
                    var cm = CommandMessageFactory.Make(r);
                    await this.crawlerQueueService.Enqueue(cm);
                }

                return Ok();
            }
            return Ok();
        }

        private void SetValuesFromPatameters(CommandMessage commandMessage, Resource resource)
        {
            if (commandMessage.Parameters.ContainsKey("display"))
            {
                resource.Display = commandMessage.Parameters["display"];
            }
            if (commandMessage.Parameters.ContainsKey("description"))
            {
                resource.Description = commandMessage.Parameters["description"];
            }
            if (commandMessage.Parameters.ContainsKey("published"))
            {
                resource.Published = DateTimeOffset.Parse(commandMessage.Parameters["published"]);
            }
        }
    }
}
