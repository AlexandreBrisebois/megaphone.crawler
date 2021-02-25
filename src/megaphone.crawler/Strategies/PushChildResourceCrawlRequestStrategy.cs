using Megaphone.Crawler.Core.Models;
using Megaphone.Crawler.Core.Services;
using Megaphone.Standard.Messages;
using System.Threading.Tasks;

namespace Megaphone.Crawler.Strategies
{
    internal class PushChildResourceCrawlRequestStrategy : Strategy<Resource>
    {
        private readonly IRestService restService;
        private readonly IAppConfig configs;

        public PushChildResourceCrawlRequestStrategy(IRestService restService, IAppConfig config)
        {
            this.restService = restService;
            this.configs = config;
        }
        internal override bool CanExecute()
        {
            return configs.ResourcePush && !string.IsNullOrEmpty(configs.CrawlMessageApiUrl);
        }

        internal async override Task ExecuteAsync(Resource model)
        {
            model.Resources.ForEach(async r =>
            {
                var message = MessageBuilder.NewCommand("crawl-request")
                                            .WithParameters("uri", model.Self.ToString())
                                            .WithParameters("display", model.Display)
                                            .WithParameters("description", model.Description)
                                            .WithParameters("published", model.Published.ToString())
                                            .Make();

                var response = await restService.PostAsync(configs.CrawlMessageApiUrl, message);
            });
        }
    }
}