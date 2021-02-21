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

namespace Megaphone.Crawler
{
    public static class CrawlerFunction
    {
        static readonly WebResourceCrawler crawler = new();

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

            log.LogInformation($"crawled ({resource.StatusCode}) : {resource.Self}");

            return new OkObjectResult(resource);
        }
    }
}
