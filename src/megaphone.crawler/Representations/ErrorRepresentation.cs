using Megaphone.Standard.Representations;
using System.Text.Json.Serialization;

namespace Megaphone.Crawler.Representations
{
    public class ErrorRepresentation : Representation
    {
        [JsonPropertyName("message")]
        public string Message { get; set; } = string.Empty;
    }
}