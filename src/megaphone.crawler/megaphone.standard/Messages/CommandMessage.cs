using System;
using System.Text.Json.Serialization;
using System.Collections.Generic;


namespace Megaphone.Standard.Messages
{
    public class CommandMessage
    {
        public CommandMessage(string name)
        {
            this.Name = name;
        }

        [JsonPropertyName("id")]
        public string Id { get; init; } = Guid.NewGuid().ToString();
        [JsonPropertyName("action")]
        public string Name { get; init; }
        [JsonPropertyName("created")]
        public DateTimeOffset Created { get; init; } = DateTimeOffset.UtcNow;
        [JsonPropertyName("parameters")]
        public Dictionary<string, string> Parameters { get; init; } = new Dictionary<string, string>();
        [JsonPropertyName("metadata")]
        public Dictionary<string, string> Metadata { get; init; } = new Dictionary<string, string>();
    }
}