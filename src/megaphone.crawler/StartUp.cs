using Megaphone.Crawler;
using Megaphone.Crawler.Core;
using Megaphone.Crawler.Services;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http;

[assembly: WebJobsStartup(typeof(StartUp))]
namespace Megaphone.Crawler
{
    class StartUp : IWebJobsStartup
    {
        public void Configure(IWebJobsBuilder builder)
        {
            var httpClient = new HttpClient();

            builder.Services.AddSingleton<IAppConfig,AppConfig>();
            builder.Services.AddSingleton<IWebResourceCrawler>(new WebResourceCrawler(httpClient));
            builder.Services.AddSingleton<IPushService>(new ResourcePushService(httpClient));
        }
    }
}