using Megaphone.Crawler.Core.Models;
using Megaphone.Crawler.Representations;
using System.Net;
using System.Threading.Tasks;

namespace Megaphone.Crawler.Strategies
{
    internal class CrawlResponseStrategy : ResponseStrategy<SystemTextJsonResult>
    {
        public CrawlResponseStrategy(AppConfig serviceContext) : base(serviceContext)
        {
        }

        internal override bool CanExecute()
        {
            return true;
        }

        internal override Task<SystemTextJsonResult> ExecuteAsync(Resource resource)
        {
            var representation = RepresentationFactory.MakeCrawlResponseRepresentation(resource);
            return Task.FromResult(new SystemTextJsonResult(representation, statusCode: HttpStatusCode.OK));
        }
    }
}