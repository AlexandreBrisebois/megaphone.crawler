using Megaphone.Crawler.Core.Models;
using Megaphone.Crawler.Representations;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Megaphone.Crawler.Strategies
{
    internal class DefaultResponseStrategy : ResponseStrategy<Resource,SystemTextJsonResult>
    {
        public DefaultResponseStrategy(IAppConfig serviceContext) : base(serviceContext)
        {
        }

        internal override bool CanExecute()
        {
            return true;
        }

        internal override Task<SystemTextJsonResult> ExecuteAsync(Resource resource)
        {
            object representation = resource.Resources.Any() ? RepresentationFactory.MakeCrawlExpandedResponseRepresentation(resource) : RepresentationFactory.MakeCrawlResponseRepresentation(resource);

            return Task.FromResult(new SystemTextJsonResult(representation, statusCode: HttpStatusCode.OK));
        }
    }
}