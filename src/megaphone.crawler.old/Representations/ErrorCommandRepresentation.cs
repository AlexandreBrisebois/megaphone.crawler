using Megaphone.Standard.Messages;
using System.Text.Json.Serialization;

namespace Megaphone.Crawler.Representations
{
    public class ErrorCommandRepresentation : ErrorRepresentation
    {
        [JsonPropertyName("command")]
        public CommandMessage Command { get; set; }
    }
}