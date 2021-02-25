using Megaphone.Crawler.Core.Models;
using Megaphone.Standard.Representations.Links;
using System.Linq;

namespace Megaphone.Crawler.Representations
{
    public partial class RepresentationFactory
    {
        public static CrawlResultRepresentation MakeCrawlResponseRepresentation(Resource resource)
        {
            var r = new CrawlResultRepresentation
            {
                Display = resource.Display,
                Url = resource.Self.ToString(),
                Created = resource.Created,
                Description = resource.Description,
                IsActive = resource.IsActive,
                Published = resource.Published,
                StatusCode = resource.StatusCode,
                Type = resource.Type
            };

            return r;
        }

        public static CrawlExpandedResultRepresentation MakeCrawlExpandedResponseRepresentation(Resource resource)
        {
            var r = new CrawlExpandedResultRepresentation
            {
                Display = resource.Display,
                Url = resource.Self.ToString(),
                Created = resource.Created,
                Description = resource.Description,
                IsActive = resource.IsActive,
                Published = resource.Published,
                StatusCode = resource.StatusCode,
                Type = resource.Type
            };

            r.Resources = resource.Resources.Select(c => MakeCrawlResponseRepresentation(c)).ToList();

            return r;
        }

        public static CrawlPushedResultRepresentation MakePushedRepresentation(Resource resource, string resourceApiUrl)
        {
            var r = new CrawlPushedResultRepresentation
            {
                Url = resource.Self.ToString(),
                Display = resource.Display,
                StatusCode = resource.StatusCode
            };

            r.AddLink(Relations.Self, $"{resourceApiUrl}/api/resources/{resource.Id}");

            return r;
        }
    }
}