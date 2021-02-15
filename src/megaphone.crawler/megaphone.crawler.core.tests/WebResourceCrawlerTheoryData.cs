using Megaphone.Crawler.Core.Models;
using System;
using Xunit;

namespace megaphone.crawler.core.tests
{
    public class WebResourceCrawlerTheoryData : TheoryData<Uri, Resource>
    {
        public WebResourceCrawlerTheoryData()
        {
            Add(new Uri("http://www.google.com"),
                new Resource("407690c8-8a65-5bb6-a884-cfbc5a8f6d4a", new Uri("http://www.google.com"))
                {
                    Display = "Google",
                    StatusCode = 200,
                    IsActive = true,
                    Type = ResourceType.Page
                });
            Add(new Uri("https://devblogs.microsoft.com/dotnet/staying-safe-with-dotnet-containers/"),
                new Resource("d64bc5a5-f2d2-572c-bfbf-b99e5340c0d9", new Uri("https://devblogs.microsoft.com/dotnet/staying-safe-with-dotnet-containers/"))
                {
                    Display = "Staying safe with .NET containers | .NET Blog",
                    StatusCode = 200,
                    IsActive = true,
                    Type = ResourceType.Page
                });
            Add(new Uri("https://devblogs.microsoft.com/dotnet/feed/"),
                new Resource("c50f7543-6b69-5e46-ae5e-bf0b82dede0e", new Uri("https://devblogs.microsoft.com/dotnet/feed/"))
                {
                    Display = ".NET Blog",
                    StatusCode = 200,
                    IsActive = true,
                    Type = ResourceType.Feed
                });
        }
    }
}
