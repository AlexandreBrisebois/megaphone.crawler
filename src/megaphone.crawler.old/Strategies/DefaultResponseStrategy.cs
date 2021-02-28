using Megaphone.Crawler.Core.Models;
using Megaphone.Crawler.Representations;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Megaphone.Crawler.Strategies
{
    internal class DefaultResponseStrategy : ResponseStrategy<Resource, JsonResult>
    {
        public DefaultResponseStrategy(IAppConfig serviceContext) : base(serviceContext)
        {
        }

        internal override bool CanExecute()
        {
            return true;
        }

        internal override Task<JsonResult> ExecuteAsync(Resource resource)
        {
            object representation = resource.Resources.Any() ? RepresentationFactory.MakeCrawlExpandedResponseRepresentation(resource) : RepresentationFactory.MakeCrawlResponseRepresentation(resource);

            return Task.FromResult(new JsonResult(representation));
        }
    }
}