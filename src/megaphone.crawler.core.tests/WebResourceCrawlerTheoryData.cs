using Megaphone.Crawler.Core.Models;
using System;
using Xunit;

namespace megaphone.crawler.core.tests
{
    public class WebResourceCrawlerTheoryData : TheoryData<string, Resource>
    {
        public WebResourceCrawlerTheoryData()
        {
            Add("http://www.google.com",
                new Resource
                {
                    Id = "407690c8-8a65-5bb6-a884-cfbc5a8f6d4a",
                    Self = "http://www.google.com",
                    Display = "Google",
                    StatusCode = 200,
                    IsActive = true,
                    Type = ResourceType.Page
                });
            Add("https://devblogs.microsoft.com/dotnet/staying-safe-with-dotnet-containers/",
                new Resource
                {
                    Id = "d64bc5a5-f2d2-572c-bfbf-b99e5340c0d9",
                    Self = "https://devblogs.microsoft.com/dotnet/staying-safe-with-dotnet-containers/",
                    Display = "Staying safe with .NET containers | .NET Blog",
                    StatusCode = 200,
                    IsActive = true,
                    Type = ResourceType.Page
                });
            Add("https://devblogs.microsoft.com/dotnet/feed/",
                new Resource
                {
                    Id = "c50f7543-6b69-5e46-ae5e-bf0b82dede0e",
                    Self = "https://devblogs.microsoft.com/dotnet/feed/",
                    Display = ".NET Blog",
                    StatusCode = 200,
                    IsActive = true,
                    Type = ResourceType.Feed
                });
            Add("https://techcommunity.microsoft.com/plugins/custom/microsoft/o365/custom-blog-rss?tid=-8440483452201197351&board=IntegrationsonAzureBlog&label=&messages=&size=10",
                new Resource
                {
                    Id = "acd11391-c716-5d64-8d43-ed3f7d6843d4",
                    Self = "https://techcommunity.microsoft.com/plugins/custom/microsoft/o365/custom-blog-rss?tid=-8440483452201197351&board=IntegrationsonAzureBlog&label=&messages=&size=10",
                    Display = "Featured Blog",
                    StatusCode = 200,
                    IsActive = true,
                    Type = ResourceType.Feed
                });
        }
    }
}
