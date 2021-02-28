using Megaphone.Crawler.Core;
using Megaphone.Crawler.Core.Models;
using System.Linq;
using System.Threading.Tasks;

namespace Megaphone.Crawler.Strategies
{
    internal class CrawlChildResourcesStrategy : Strategy<Resource>
    {
        private readonly IWebResourceCrawler crawler;
        private readonly IAppConfig configs;

        public CrawlChildResourcesStrategy(IWebResourceCrawler crawler, IAppConfig config)
        {
            this.crawler = crawler;
            this.configs = config;
        }
        internal override bool CanExecute()
        {
            return !configs.ResourcePush;
        }

        internal async override Task ExecuteAsync(Resource model)
        {
            var childResources = model.Resources.AsParallel().Select(async r =>
            {
                var childResource = await crawler.GetResourceAsync(r.Self);

                childResource.Display = r.Display;
                childResource.Description = r.Description;
                childResource.Published = r.Published;

                return childResource;
            }).ToArray();

            Task.WaitAll(childResources);

            childResources.Select(t => t.Result).ToList().ForEach(c =>
            {
                model.Resources.RemoveAll(r => r.Id == c.Id);
                model.Resources.Add(c);
            });
        }
    }
}