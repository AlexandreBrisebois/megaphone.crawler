using System;
using System.Text.Json.Serialization;
using System.Collections.Generic;

namespace Megaphone.Standard.Events
{

    public class Event
    {
        public Event(string name)
        {
            Name = name;
        }

        [JsonPropertyName("id")]
        public string Id { get; init; } = Guid.NewGuid().ToString();
        [JsonPropertyName("name")]
        public string Name { get; init; }
        [JsonPropertyName("created")]
        public DateTimeOffset Created { get; init; } = DateTimeOffset.UtcNow;
        [JsonPropertyName("metadata")]
        public Dictionary<string, string> Metadata { get; init; } = new Dictionary<string, string>();
        [JsonPropertyName("data")]
        public Dictionary<string, Dictionary<string, string>> Data { get; init; } = new Dictionary<string, Dictionary<string, string>>();
    }
}