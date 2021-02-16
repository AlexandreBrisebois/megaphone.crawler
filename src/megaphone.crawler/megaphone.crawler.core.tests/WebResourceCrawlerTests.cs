using Megaphone.Crawler.Core;
using Megaphone.Crawler.Core.Models;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace megaphone.crawler.core.tests
{
    public class WebResourceCrawlerTests
    {
        public static TheoryData data => new WebResourceCrawlerTheoryData();

        [Theory(DisplayName = "Create Resource from Uri")]
        [MemberData(nameof(data))]
        public async Task CrawlUri(Uri uri, Resource expectedResource)
        {
            WebResourceCrawler crawler = new();
            var resource = await crawler.GetResourceAsync(uri);

            Assert.IsType<Resource>(resource);

            Assert.Equal(expectedResource.Id, resource.Id);
            Assert.Equal(expectedResource.Display, resource.Display);
            Assert.Equal(expectedResource.Self, resource.Self);
            Assert.Equal(expectedResource.IsActive, resource.IsActive);
            Assert.Equal(expectedResource.StatusCode, resource.StatusCode);
            Assert.Equal(expectedResource.Type, resource.Type);

            if (resource.Type == ResourceType.Feed)
                Assert.True(resource.Resources.Any());
        }
    }
}
