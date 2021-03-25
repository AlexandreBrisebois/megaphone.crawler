using Dapr.Client;
using Megaphone.Crawler.Commands;
using Megaphone.Crawler.Core;
using Megaphone.Crawler.Core.Models;
using Megaphone.Crawler.Queries;
using Megaphone.Standard.Messages;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Diagnostics;
using System.Reflection.Metadata;
using System.Threading.Tasks;

namespace Megaphone.Crawler.Controllers
{
    [ApiController]
    [Route("/")]
    public class CrawlRequestProcessor : ControllerBase
    {
        private readonly IWebResourceCrawler crawler;
        private readonly DaprClient daprClient;

        public CrawlRequestProcessor([FromServices] IWebResourceCrawler crawler,
                                     [FromServices] DaprClient daprClient)
        {
            this.crawler = crawler;
            this.daprClient = daprClient;
        }

        private async Task<bool> ShouldSkipCrawl(string uri)
        {
            var q = new GetResourceLastUpdateQuery(uri);
            var resource = await q.ExecuteAsync(daprClient);

            if (resource.LastUpdated == DateTimeOffset.MinValue)
                return false;

            if(resource.Type == ResourceType.Feed)
            {
                return false;
            }

            if(DateTimeOffset.UtcNow > resource.LastUpdated.AddHours(2))
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
                {
                    return Ok();
                }

                var resource = await crawler.GetResourceAsync(uri);

                if (Debugger.IsAttached)
                    Console.WriteLine($"crawler : \"{resource.Display}\" : {uri}");

                SetValuesFromPatameters(message, resource);

                var postResource = new PostResourceCommand(resource);
                await postResource.ApplyAsync(daprClient);

                if (Debugger.IsAttached)
                    Console.WriteLine($"posted resource : \"{resource.Display}\"");

                foreach (var r in resource.Resources)
                {
                    var c = new SendCrawlRequestCommand(r);
                    await c.ApplyAsync(daprClient);

                    if (Debugger.IsAttached)
                        Console.WriteLine($"request crawl : {r.Self}");
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
