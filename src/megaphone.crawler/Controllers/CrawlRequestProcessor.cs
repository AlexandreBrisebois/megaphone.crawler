﻿using Dapr.Client;
using Megaphone.Crawler.Commands;
using Megaphone.Crawler.Core;
using Megaphone.Crawler.Core.Models;
using Megaphone.Standard.Messages;
using Microsoft.AspNetCore.Mvc;
using System;
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

        [HttpPost("crawl-requests")]
        public async Task<IActionResult> PostAsync(CommandMessage message)
        {
            if (message.Action == "crawl-request")
            {
                var resource = await crawler.GetResourceAsync(message.Parameters["uri"]);
                
                SetValuesFromPatameters(message, resource);

                var postResource = new PostResourceCommand(resource);
                await postResource.ApplyAsync(daprClient);

                foreach (var r in resource.Resources)
                {
                    var c = new SendCrawlRequestCommand(r);
                    await c.ApplyAsync(daprClient);
                }
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
