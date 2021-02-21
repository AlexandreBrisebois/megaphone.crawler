using System;
using System.Collections.Generic;

namespace Megaphone.Crawler.Core.Models
{
    public class Resource
    {
        public Resource(string id, Uri uri)
        {
            Id = id;
            Self = uri;
        }

        public string Id { get; init; }
        public string Display { get; set; } = string.Empty;
        public int StatusCode { get; set; }
        public DateTimeOffset Created { get; init; } = DateTimeOffset.UtcNow;
        public bool IsActive { get; set; }
        public string Type { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public Uri Self { get; init; }
        public DateTimeOffset Published { get; set; }
        public string Cache { get; set; } = string.Empty;
        public List<Resource> Resources { get; init; } = new List<Resource>();
    }
}
