using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Megaphone.Standard.Messages;
using Megaphone.Crawler.Core;
using Megaphone.Crawler.Core.Extensions;
using System;
using Megaphone.Crawler.Core.Models;
using System.Net.Http;
using System.Collections.Generic;

namespace Megaphone.Crawler
{
    public static class CrawlerFunction
    {
        static readonly HttpClient httpClient = new();
        static readonly WebResourceCrawler crawler = new(httpClient);

        static readonly string resourceApi = Environment.GetEnvironmentVariable("MEGAPHONE_RESOURCE_API_URL");

        [FunctionName("crawl")]
        public static async Task<ObjectResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "")] HttpRequest req,
            ILogger log)
        {
            string requestBody = new StreamReader(req.Body).ReadToEnd();
            var commandMessage = JsonConvert.DeserializeObject<CommandMessage>(requestBody);

            if (commandMessage.Name != "crawl-request")
            {
                return new BadRequestObjectResult(new
                {
                    Message = "unsupported command",
                    Command = commandMessage
                });
            }

            var resource = await crawler.GetResourceAsync(commandMessage.Parameters["uri"].ToUri());
            SetValuesFromPatameters(commandMessage, resource);

            log.LogInformation($"crawled ({resource.StatusCode}) : {resource.Self}");
            
            await LoadChildResouces(resource);

            return new OkObjectResult(resource);
        }

        private static async Task LoadChildResouces(Resource resource)
        {
            var childResources = new List<Resource>();

            foreach(var r in resource.Resources)
            {
                var childResource = await GetChildResource(r);
                if (childResource != Resource.Empty)
                    childResources.Add(childResource);
            }

            foreach(var cr in childResources)
            {
                resource.Resources.RemoveAll(r => r.Id == cr.Id);
                resource.Resources.Add(cr);
            }           
        }

        private static async Task<Resource> GetChildResource(Resource r)
        {

            var  resource =  await crawler.GetResourceAsync(r.Self);
            resource.Display = r.Display;
            resource.Description = r.Description;
            resource.Published = r.Published;

            return resource;

            // #TODO: use this in Resource Service

            //var commandMessage = MessageBuilder.NewCommand("crawl-request")
            //          .WithParameters("uri", r.Self.AbsoluteUri)
            //          .WithParameters("id", r.Id)
            //          .WithParameters("display", r.Display)
            //          .WithParameters("description", r.Description)
            //          .WithParameters("published", r.Published.ToString("s"))
            //          .Make();

            //var response = await httpClient.PostAsync(crawlApiUrl, new StringContent(JsonConvert.SerializeObject(commandMessage)));

            //if (response.StatusCode == System.Net.HttpStatusCode.OK)
            //{
            //    var content = await response.Content.ReadAsStringAsync();
            //    var resource = JsonConvert.DeserializeObject<Resource>(content);
            //    return resource;
            //}
            //return Resource.Empty;
        }

        private static void SetValuesFromPatameters(CommandMessage commandMessage, Resource resource)
        {
            if (commandMessage.Parameters.Count > 1)
            {
                resource.Display = commandMessage.Parameters["display"];
                resource.Description = commandMessage.Parameters["description"];
                resource.Published = DateTimeOffset.Parse(commandMessage.Parameters["published"]);
            }
        }
    }
}
