using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Megaphone.Crawler.Representations
{
    public class CrawlExpandedResultRepresentation : CrawlResultRepresentation
    {
        [JsonPropertyName("resources")]
        public List<CrawlResultRepresentation> Resources { get; set; } = new();
    }
}