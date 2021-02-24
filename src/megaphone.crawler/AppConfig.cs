using Microsoft.WindowsAzure.Storage.Blob.Protocol;
using System;
using System.Collections.Generic;

namespace Megaphone.Crawler
{
    public interface IAppConfig
    {
        string ResourceApiUrl { get; }
        bool ResourcePush { get; }
    }

    public class AppConfig : IAppConfig
    {
        Dictionary<string, object> configs = new();

        public AppConfig()
        {
            ResourceApiUrl = Environment.GetEnvironmentVariable("MEGAPHONE_RESOURCE_API_URL");
            ResourcePush = bool.Parse(Environment.GetEnvironmentVariable("MEGAPHONE_RESOURCE_PUSH") ?? "false");
        }

        public string ResourceApiUrl { get; private set; }
        public bool ResourcePush { get; private set; }
    }
}