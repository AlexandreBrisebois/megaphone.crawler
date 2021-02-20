using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Megaphone.Standard.Messages;
using Megaphone.Crawler.Core;
using Megaphone.Crawler.Core.Extensions;
using Microsoft.Azure.Functions.Worker;
using System.Net;
using System.Collections.Generic;
using System.Text.Json;
using System.Text;

namespace megaphone.crawler
{
    public static class CrawlerFunction
    {
        static WebResourceCrawler crawler = new();

        [FunctionName("Crawl")]
        public static async Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequestData req)
        {
            string requestBody = Encoding.UTF8.GetString(req.Body.Value.Span);

            var commandMessage = JsonSerializer.Deserialize<CommandMessage>(requestBody);

            var headers = new Dictionary<string, string>();
            headers.Add("Content", "Content - Type: application / json; charset = utf - 8");

            if (commandMessage.Name != "crawl-request")
            {
                return new HttpResponseData(HttpStatusCode.BadRequest)
                {
                     Headers = headers,
                     Body = JsonSerializer.Serialize(commandMessage)
                };
            }

            var resource = await crawler.GetResourceAsync(commandMessage.Parameters["uri"].ToUri());

            var response = new HttpResponseData(HttpStatusCode.OK)
            {
                Headers = headers,
                Body = JsonSerializer.Serialize(resource)
            };

            return response;
        }
    }
}
