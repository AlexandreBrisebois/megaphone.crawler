using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json;

namespace Megaphone.Crawler
{
    public class SystemTextJsonResult : ContentResult
    {
        private const string ContentTypeApplicationJson = "application/json";

        public SystemTextJsonResult(object value, JsonSerializerOptions options = null, HttpStatusCode statusCode = HttpStatusCode.OK)
        {
            StatusCode = (int)statusCode;
            ContentType = ContentTypeApplicationJson;
            Content = options == null ? JsonSerializer.Serialize(value) : JsonSerializer.Serialize(value, options);
        }
    }
}