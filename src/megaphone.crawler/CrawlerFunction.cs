using Megaphone.Crawler.Core;
using Megaphone.Crawler.Core.Extensions;
using Megaphone.Crawler.Core.Models;
using Megaphone.Crawler.Core.Services;
using Megaphone.Crawler.Representations;
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
        private readonly List<ResponseStrategy<Resource, SystemTextJsonResult>> responseStrategies;
        private readonly List<Strategy<Resource>> childResourceStrategy;

        public CrawlerFunction(IAppConfig configs,
                               IWebResourceCrawler crawler,
                               IRestService restService)
        {
            this.crawler = crawler;

            childResourceStrategy = new()
            {
                new PushChildResourceCrawlRequestStrategy(restService, configs),
                new CrawlChildResourcesStrategy(crawler, configs)
            };

            responseStrategies = new()
            {
                new PushResourceStrategy(restService, configs),
                new DefaultResponseStrategy(configs)
            };
        }

        [FunctionName("crawl")]
        public async Task<SystemTextJsonResult> Crawl([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "crawl")] HttpRequest req,
                                                      ILogger log)
        {
            string requestBody = new StreamReader(req.Body).ReadToEnd();
            var commandMessage = JsonSerializer.Deserialize<CommandMessage>(requestBody);

            if (commandMessage.Action != "crawl-request")
            {
                return new SystemTextJsonResult(new ErrorCommandRepresentation
                {
                    Message = $"unsupported command",
                    Command = commandMessage
                }, statusCode: HttpStatusCode.BadRequest);
            }

            var resource = await crawler.GetResourceAsync(commandMessage.Parameters["uri"]);

            SetValuesFromPatameters(commandMessage, resource);

            log.LogInformation($"crawled ({resource.StatusCode}) : {resource.Self}");

            try
            {
                await childResourceStrategy.First(s => s.CanExecute())
                                           .ExecuteAsync(resource);
            }catch
            {
                return new SystemTextJsonResult(new ErrorRepresentation
                {
                    Message = "failed to process child resources, check urls provided in app configurations.",
                }, statusCode: HttpStatusCode.BadRequest);
            }

            return await responseStrategies.First(s => s.CanExecute()).ExecuteAsync(resource);
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