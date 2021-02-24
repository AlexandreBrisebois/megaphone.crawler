using Megaphone.Crawler.Core;
using Megaphone.Crawler.Services;
using Megaphone.Crawler.Tests.Data;
using Megaphone.Crawler.Tests.Mocks;
using Megaphone.Standard.Messages;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using Xunit;

namespace Megaphone.Crawler.Tests
{
    public class CrawlerFunctionTests
    {
        private readonly ILogger logger = TestFactory.CreateLogger();

        private readonly MockAppConfig NoPushServiceConfig = new MockAppConfig(resourcePush: false,
                                                                                resourceApiUrl: null);

        public static readonly GoodRequestData goodRequestData = new();

        [Theory]
        [MemberData(nameof(goodRequestData))]
        public async void OkRequestTest(object body, string expectedType)
        {
            MockPushService mockPushService = new MockPushService();

            var input = (CommandMessage)body;

            CrawlerFunction func = new(NoPushServiceConfig,
                                       new WebResourceCrawler(new HttpClient()),
                                       mockPushService);

            SystemTextJsonResult? response = await func.Crawl(TestFactory.CreateHttpRequest(input),                                                                        
                                                              logger);

            Assert.IsType<SystemTextJsonResult>(response);

            var resource = JsonSerializer.Deserialize<Representations.CrawlResultRepresentation>(response.Content);

            Assert.Equal(expectedType, resource.Type);

            if (input.Parameters.Count > 1)
            {
                Assert.Equal(input.Parameters["display"], resource.Display);
                Assert.Equal(input.Parameters["description"], resource.Description);
                Assert.Equal(DateTimeOffset.Parse(input.Parameters["published"]), resource.Published);
            }

            Assert.False(mockPushService.WasCalled);
        }

        [Fact]
        public async void CrawlAndExapndChildResouces()
        {
            MockPushService mockPushService = new MockPushService();

            var input = MessageBuilder.NewCommand("crawl-request")
                                      .WithParameters("uri", "https://blogs.msdn.microsoft.com/dotnet/feed")
                                      .WithParameters("expand", "child-resources")
                                      .Make();

            CrawlerFunction func = new(NoPushServiceConfig,
                                       new WebResourceCrawler(new HttpClient()),
                                       mockPushService);

            SystemTextJsonResult? response = await func.Crawl(TestFactory.CreateHttpRequest(input),
                                                              logger);

            Assert.IsType<SystemTextJsonResult>(response);

            var resource = JsonSerializer.Deserialize<Representations.CrawlExpandedResultRepresentation>(response.Content);

            Assert.True(resource.Resources.All(r => !string.IsNullOrEmpty(r.Description)));

            Assert.False(mockPushService.WasCalled);
        }

        public static readonly BadRequestData badRequestData = new();

        [Theory]
        [MemberData(nameof(badRequestData))]
        public async void BadRequestTest(object body)
        {
            CrawlerFunction func = new(NoPushServiceConfig,
                                       new WebResourceCrawler(new HttpClient()),
                                        new ResourcePushService(new HttpClient()));

            SystemTextJsonResult? response = await func.Crawl(TestFactory.CreateHttpRequest(body),
                                                              logger);
            Assert.Equal(400, response.StatusCode);
        }

        private readonly MockAppConfig NoPushDefaultUrlServiceContext = new MockAppConfig(resourcePush: true,
                                                                                          resourceApiUrl: "http://localhost:80");

        [Fact]
        public async void CrawlPush()
        {
            MockPushService mockPushService = new MockPushService();

            var input = MessageBuilder.NewCommand("crawl-request")
                                      .WithParameters("uri", "https://devblogs.microsoft.com/dotnet/feed/")
                                      .Make();

            CrawlerFunction func = new(NoPushDefaultUrlServiceContext,
                                     new WebResourceCrawler(new HttpClient()),
                                     mockPushService);

            SystemTextJsonResult? response = await func.Crawl(TestFactory.CreateHttpRequest(input),
                                                              logger);

            Assert.IsType<SystemTextJsonResult>(response);

            var resource = JsonSerializer.Deserialize<Representations.CrawlResultRepresentation>(response.Content);

            Assert.Equal("https://devblogs.microsoft.com/dotnet/feed/", resource.Url);
            Assert.True(mockPushService.WasCalled);
        }

        private readonly MockAppConfig ErrorPushServiceConfig = new MockAppConfig(resourcePush: true,
                                                                                   resourceApiUrl: "http://domain.com");

        [Fact]
        public async void CrawlFailPushDefaultUrl()
        {
            var input = MessageBuilder.NewCommand("crawl-request")
                                      .WithParameters("uri", "https://blogs.msdn.microsoft.com/dotnet/feed")
                                      .Make();

            CrawlerFunction func = new(ErrorPushServiceConfig,
                                       new WebResourceCrawler(new HttpClient()),
                                       new ResourcePushService(new HttpClient()));

            SystemTextJsonResult? response = await func.Crawl(TestFactory.CreateHttpRequest(input),
                                                              logger);

            Assert.IsType<SystemTextJsonResult>(response);

            var resource = JsonSerializer.Deserialize<Representations.ErrorPushRepresentation>(response.Content);

            Assert.Equal(404, resource.StatusCode);

        }

        private readonly MockAppConfig PushServiceConfig = new MockAppConfig(resourcePush: true,
                                                                             resourceApiUrl: "http://domain.com");

        [Fact]
        public async void CrawlPushDefaultUrl()
        {
            MockPushService mockPushService = new MockPushService();

            var input = MessageBuilder.NewCommand("crawl-request")
                                      .WithParameters("uri", "https://blogs.msdn.microsoft.com/dotnet/feed")
                                      .Make();

            CrawlerFunction func = new(PushServiceConfig,
                                       new WebResourceCrawler(new HttpClient()),
                                       mockPushService);

            SystemTextJsonResult? response = await func.Crawl(TestFactory.CreateHttpRequest(input),
                                                              logger);

            Assert.IsType<SystemTextJsonResult>(response);

            var resource = JsonSerializer.Deserialize<Representations.CrawlPushedResultRepresentation>(response.Content);

            Assert.False(string.IsNullOrEmpty(resource.Display));
            Assert.True(mockPushService.WasCalled);
        }
    }
}