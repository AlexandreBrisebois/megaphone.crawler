using Megaphone.Standard.Representations;
using System.Text.Json.Serialization;

namespace Megaphone.Crawler.Representations
{
    public class CrawlPushedResultRepresentation : Representation
    {
        [JsonPropertyName("display")]
        public string Display { get; set; } = string.Empty;
        [JsonPropertyName("url")]
        public string Url { get; set; } = string.Empty;
        [JsonPropertyName("status-code")]
        public int StatusCode { get; set; }
    }
}