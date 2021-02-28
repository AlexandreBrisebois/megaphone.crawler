using System;
using System.Collections.Generic;

namespace Megaphone.Crawler
{
    public class AppConfig : IAppConfig
    {
        public AppConfig()
        {
            CrawlMessageApiUrl = Environment.GetEnvironmentVariable("MEGAPHONE_CRAWL_MESSAGE_API_URL");
            ResourceApiUrl = Environment.GetEnvironmentVariable("MEGAPHONE_RESOURCE_API_URL");
            ResourcePush = bool.Parse(Environment.GetEnvironmentVariable("MEGAPHONE_RESOURCE_PUSH") ?? "false");
        }

        public string? CrawlMessageApiUrl { get; private set; }
        public string? ResourceApiUrl { get; private set; }
        public bool ResourcePush { get; private set; }
    }
}