using System.Text.Json.Serialization;

namespace Megaphone.Crawler.Representations
{
    public class ErrorPushRepresentation : ErrorRepresentation

    {
        [JsonPropertyName("status-code")]
        public int StatusCode { get; set; }
    }
}