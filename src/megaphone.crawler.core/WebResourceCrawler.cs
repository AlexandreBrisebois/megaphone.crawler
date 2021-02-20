using Megaphone.Crawler.Core.Models;
using Megaphone.Crawler.Core.Queires;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Megaphone.Crawler.Core
{
    public class WebResourceCrawler : Crawler<Resource>
    {
        private static readonly HttpClient Client = new();

        public override async Task<Resource> GetResourceAsync(Uri uri)
        {
            var query = new GetResourceFromUri(Client);
            return await query.ExecuteAsync(uri);
        }
    }
}
