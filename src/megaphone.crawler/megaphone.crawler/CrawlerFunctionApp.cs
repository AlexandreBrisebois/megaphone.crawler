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

namespace megaphone.crawler
{
    public static class CrawlerFunctionApp
    {
        static WebResourceCrawler crawler = new();

        [FunctionName("Crawl")]
        public static async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req, ILogger log)
        {
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var commandMessage = JsonConvert.DeserializeObject<CommandMessage>(requestBody);

            if (commandMessage.Name != "crawl-request")
                return new BadRequestObjectResult(new
                {
                    Message = "bad command",
                    Command = commandMessage
                });

            var resource = await crawler.GetResourceAsync(commandMessage.Parameters["uri"].ToUri());

            return new OkObjectResult(resource);
        }
    }
}
