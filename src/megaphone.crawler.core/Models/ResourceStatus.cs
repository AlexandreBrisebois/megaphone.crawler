using System;

namespace Megaphone.Crawler.Core.Models
{
    public class ResourceStatus
    {
        public bool IsActive { get; set; } = false;
        public string Type { get; set; } = string.Empty;
        public DateTimeOffset LastUpdated { get; set; }
    }
}
