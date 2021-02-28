using Dapr.AzureFunctions.Extension;
using Megaphone.Crawler.Core;
using Megaphone.Crawler.Core.Models;
using Megaphone.Crawler.Core.Services;
using Megaphone.Crawler.Representations;
using Megaphone.Crawler.Strategies;
using Megaphone.Standard.Messages;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Megaphone.Crawler
{
    public class CrawlerFunction
    {
        private readonly IAppConfig configs;
        private readonly IWebResourceCrawler crawler;

        public CrawlerFunction(IAppConfig configs,
                               IWebResourceCrawler crawler,
                               IRestService restService)
        {
            this.configs = configs;
            this.crawler = crawler;
        }

        [FunctionName("crawl-queue-processor")]
        public async Task<ActionResult> CrawlQueueProcessor([DaprBindingTrigger(BindingName = "crawl-queue")] JObject triggerData,
                                                                    ILogger log)
        {
            var commandMessage = triggerData.ToObject<CommandMessage>();

            if (commandMessage.Action != "crawl-request")
            {
                return new BadRequestObjectResult(new ErrorCommandRepresentation
                {
                    Message = $"unsupported command",
                    Command = commandMessage
                });
            }

            var resource = await crawler.GetResourceAsync(commandMessage.Parameters["uri"]);

            SetValuesFromPatameters(commandMessage, resource);

            log.LogInformation($"crawled ({resource.StatusCode}) : {resource.Self}");

            return new OkResult();
            //try
            //{
            //    await childResourceStrategy.First(s => s.CanExecute())
            //                               .ExecuteAsync(resource);
            //}
            //catch
            //{
            //    return new SystemTextJsonResult(new ErrorRepresentation
            //    {
            //        Message = "failed to process child resources, check urls provided in app configurations.",
            //    }, statusCode: HttpStatusCode.BadRequest);
            //}

            //return await responseStrategies.First(s => s.CanExecute()).ExecuteAsync(resource);
        }

        [FunctionName("crawl")]
        public async Task<ActionResult> Crawl([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "crawl")] HttpRequest req,
                                                      ILogger log)
        {
            string requestBody = new StreamReader(req.Body).ReadToEnd();
            var commandMessage = JsonConvert.DeserializeObject<CommandMessage>(requestBody);

            if (commandMessage.Action != "crawl-request")
            {
                return new BadRequestObjectResult(new ErrorCommandRepresentation
                {
                    Message = $"unsupported command",
                    Command = commandMessage
                });
            }

            var resource = await crawler.GetResourceAsync(commandMessage.Parameters["uri"]);

            SetValuesFromPatameters(commandMessage, resource);

            log.LogInformation($"crawled ({resource.StatusCode}) : {resource.Self}");

            try
            {
                var strategy = new CrawlChildResourcesStrategy(crawler, configs);
                await strategy.ExecuteAsync(resource);
            }
            catch
            {
                return new BadRequestObjectResult(new ErrorRepresentation
                {
                    Message = "failed to process child resources, check urls provided in app configurations.",
                });
            }

            var responseStrategy = new DefaultResponseStrategy(configs);

            return await responseStrategy.ExecuteAsync(resource);
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