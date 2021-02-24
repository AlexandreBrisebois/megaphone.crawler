using Megaphone.Crawler.Core.Models;
using Megaphone.Crawler.Core.Queires;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Megaphone.Crawler.Core
{
    public class WebResourceCrawler : Crawler<Resource>, IWebResourceCrawler
    {
        private readonly HttpClient httpClient;

        public WebResourceCrawler(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public override async Task<Resource> GetResourceAsync(Uri uri)
        {
            var query = new GetResourceFromUri(httpClient);
            return await query.ExecuteAsync(uri);
        }
    }
}
