using Megaphone.Crawler.Core.Models;
using Megaphone.Crawler.Core.Queires;
using Megaphone.Crawler.Core.Services;
using System.Threading.Tasks;

namespace Megaphone.Crawler.Core
{
    public class WebResourceCrawler : Crawler<Resource>, IWebResourceCrawler
    {
        private readonly IRestService service;

        public WebResourceCrawler(IRestService service)
        {
            this.service = service;
        }

        public override async Task<Resource> GetResourceAsync(string uri)
        {
            var query = new GetResourceFromUri(service);
            return await query.ExecuteAsync(uri);
        }
    }
}
