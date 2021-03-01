using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Megaphone.Crawler.Core.Models
{
    public class Resource
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }
        [JsonPropertyName("display")]
        public string Display { get; set; } = string.Empty;
        [JsonPropertyName("status-code")]
        public int StatusCode { get; set; }
        [JsonPropertyName("created")]
        public DateTimeOffset Created { get; init; } = DateTimeOffset.UtcNow;
        [JsonPropertyName("is-active")]
        public bool IsActive { get; set; }
        [JsonPropertyName("type")]
        public string Type { get; set; } = string.Empty;
        [JsonPropertyName("description")]
        public string Description { get; set; } = string.Empty;
        [JsonPropertyName("self")]
        public string Self { get; set; }
        [JsonPropertyName("published")]
        public DateTimeOffset Published { get; set; }
        [JsonPropertyName("cache")]
        public string Cache { get; set; } = string.Empty;
        [JsonPropertyName("resources")]
        public List<Resource> Resources { get; init; } = new List<Resource>();
        
        public static Resource Empty = new();
    }
}
