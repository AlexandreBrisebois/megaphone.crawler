using Megaphone.Standard.Representations;
using System;
using System.Text.Json.Serialization;

namespace Megaphone.Crawler.Representations
{
    public class CrawlResultRepresentation : Representation
    {
        [JsonPropertyName("display")]
        public string Display { get; set; } = string.Empty;
        [JsonPropertyName("url")]
        public string Url { get; set; } = string.Empty;
        [JsonPropertyName("status-code")]
        public int StatusCode { get; set; } = 0;
        [JsonPropertyName("created")]
        public DateTimeOffset Created { get; set; } = DateTimeOffset.UtcNow;
        [JsonPropertyName("is-active")]
        public bool IsActive { get; set; } = false;
        [JsonPropertyName("type")]
        public string Type { get; set; } = string.Empty;
        [JsonPropertyName("description")]
        public string Description { get; set; } = string.Empty;
        [JsonPropertyName("published")]
        public DateTimeOffset Published { get; set; }
    }
}