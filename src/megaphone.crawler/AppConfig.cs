using System;
using System.Collections.Generic;

namespace Megaphone.Crawler
{ 
    public class AppConfig
    {
        Dictionary<string, object> configs = new();

        public AppConfig()
        {
            Set("resourceApiUrl", Environment.GetEnvironmentVariable("MEGAPHONE_RESOURCE_API_URL"));
            Set("resourcePush", bool.Parse(Environment.GetEnvironmentVariable("MEGAPHONE_RESOURCE_PUSH") ?? "false"));
        }

        protected void Set<T>(string key, T value) => configs[key] = value;

        protected T Get<T>(string key) => (T)configs[key];

        protected string Get(string key) => configs[key] as string;

        public bool HasConfig(string key) => configs.ContainsKey(key) && configs[key] != null;

        public string ResourceApiUrl => Get("resourceApiUrl");
        public bool ResourcePush => Get<bool>("resourcePush");
    }
}