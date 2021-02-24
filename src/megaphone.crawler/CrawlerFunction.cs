using Megaphone.Crawler.Core;
using Megaphone.Crawler.Core.Extensions;
using Megaphone.Crawler.Core.Models;
using Megaphone.Crawler.Representations;
using Megaphone.Crawler.Services;
using Megaphone.Crawler.Strategies;
using Megaphone.Standard.Messages;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace Megaphone.Crawler
{
    public class CrawlerFunction
    {
        private readonly IWebResourceCrawler crawler;
        private readonly List<ResponseStrategy<SystemTextJsonResult>> responseStrategies;

        public CrawlerFunction(IAppConfig configs,
                               IWebResourceCrawler crawler,
                               IPushService pushService)
        {
            this.crawler = crawler;

            responseStrategies = new()
            {
                new ResourcePushStrategy(pushService, configs),
                new CrawlResponseStrategy(configs)
            };
        }

        [FunctionName("crawl")]
        public async Task<SystemTextJsonResult> Crawl(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "crawl")] HttpRequest req,
            ILogger log)
        {
            string requestBody = new StreamReader(req.Body).ReadToEnd();
            var commandMessage = JsonSerializer.Deserialize<CommandMessage>(requestBody);

            if (commandMessage.Name != "crawl-request")
            {
                return new SystemTextJsonResult(new ErrorCommandRepresentation
                {
                    Message = $"unsupported command",
                    Command = commandMessage
                }, statusCode: HttpStatusCode.BadRequest);
            }

            var resource = await crawler.GetResourceAsync(commandMessage.Parameters["uri"].ToUri());

            SetValuesFromPatameters(commandMessage, resource);

            log.LogInformation($"crawled ({resource.StatusCode}) : {resource.Self}");

            if (commandMessage.Parameters.TryGetValue("expand", out string p))
                if (p == "child-resources")
                    await LoadChildResouces(resource);

            return await responseStrategies.First(s => s.CanExecute()).ExecuteAsync(resource);
        }

        private async Task LoadChildResouces(Resource resource)
        {
            var childResources = new List<Resource>();

            foreach (var r in resource.Resources)
            {
               var childResource = await crawler.GetResourceAsync(r.Self);

                childResource.Display = r.Display;
                childResource.Description = r.Description;
                childResource.Published = r.Published;

                if (childResource != Resource.Empty)
                    childResources.Add(childResource);
            }

            foreach (var cr in childResources)
            {
                resource.Resources.RemoveAll(r => r.Id == cr.Id);
                resource.Resources.Add(cr);
            }
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
            if (commandMessage.Parameters.ContainsKey("display"))
            {
                resource.Published = DateTimeOffset.Parse(commandMessage.Parameters["published"]);
            }
        }
    }
}