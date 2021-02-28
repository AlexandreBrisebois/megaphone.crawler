using Megaphone.Crawler.Core;
using Megaphone.Crawler.Core.Services;
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
    //public class CrawlerFunctionTests
    //{
    //    private readonly ILogger logger = TestFactory.CreateLogger();

    //    private readonly MockAppConfig NoPushServiceConfig = new MockAppConfig(resourcePush: false,
    //                                                                           resourceApiUrl: null,
    //                                                                           crawlMessageApiUrl: null);

    //    public static readonly GoodRequestData goodRequestData = new();

    //    [Theory]
    //    [MemberData(nameof(goodRequestData))]
    //    public async void OkRequestTest(object body, string expectedType)
    //    {
    //        MockRestService mockPushService = new MockRestService();

    //        var input = (CommandMessage)body;

    //        CrawlerFunction func = new(NoPushServiceConfig,
    //                                   new WebResourceCrawler(new RestService(new HttpClient())),
    //                                   mockPushService);

    //        SystemTextJsonResult? response = await func.Crawl(TestFactory.CreateHttpRequest(input),                                                                        
    //                                                          logger);

    //        Assert.IsType<SystemTextJsonResult>(response);

    //        var resource = JsonSerializer.Deserialize<Representations.CrawlResultRepresentation>(response.Content);

    //        Assert.Equal(expectedType, resource.Type);

    //        if (input.Parameters.Count > 1)
    //        {
    //            Assert.Equal(input.Parameters["display"], resource.Display);
    //            Assert.Equal(input.Parameters["description"], resource.Description);
    //            Assert.Equal(DateTimeOffset.Parse(input.Parameters["published"]), resource.Published);
    //        }

    //        Assert.False(mockPushService.PostWasCalled);
    //    }

    //    [Fact]
    //    public async void CrawlAndExapndChildResouces()
    //    {
    //        MockRestService mockPushService = new MockRestService();

    //        var input = MessageBuilder.NewCommand("crawl-request")
    //                                  .WithParameters("uri", "https://blogs.msdn.microsoft.com/dotnet/feed")
    //                                  .Make();

    //        CrawlerFunction func = new(NoPushServiceConfig,
    //                                   new WebResourceCrawler(new RestService(new HttpClient())),
    //                                   mockPushService);

    //        SystemTextJsonResult? response = await func.Crawl(TestFactory.CreateHttpRequest(input),
    //                                                          logger);

    //        Assert.IsType<SystemTextJsonResult>(response);

    //        var resource = JsonSerializer.Deserialize<Representations.CrawlExpandedResultRepresentation>(response.Content);

    //        Assert.True(resource.Resources.All(r => !string.IsNullOrEmpty(r.Description)));

    //        Assert.False(mockPushService.PostWasCalled);
    //    }

    //    public static readonly BadRequestData badRequestData = new();

    //    [Theory]
    //    [MemberData(nameof(badRequestData))]
    //    public async void BadRequestTest(object body)
    //    {
    //        CrawlerFunction func = new(NoPushServiceConfig,
    //                                   new WebResourceCrawler(new RestService(new HttpClient())),
    //                                   new RestService(new HttpClient()));

    //        SystemTextJsonResult? response = await func.Crawl(TestFactory.CreateHttpRequest(body),
    //                                                          logger);
    //        Assert.Equal(400, response.StatusCode);
    //    }

    //    private readonly MockAppConfig NoPushDefaultUrlServiceContext = new MockAppConfig(resourcePush: true,
    //                                                                                      resourceApiUrl: "http://domain.com",
    //                                                                                      crawlMessageApiUrl: "http://domain.com");

    //    [Fact]
    //    public async void CrawlPush()
    //    {
    //        MockRestService mockPushService = new MockRestService();

    //        var input = MessageBuilder.NewCommand("crawl-request")
    //                                  .WithParameters("uri", "https://devblogs.microsoft.com/dotnet/feed/")
    //                                  .Make();

    //        CrawlerFunction func = new(NoPushDefaultUrlServiceContext,
    //                                 new WebResourceCrawler(new RestService(new HttpClient())),
    //                                 mockPushService);

    //        SystemTextJsonResult? response = await func.Crawl(TestFactory.CreateHttpRequest(input),
    //                                                          logger);

    //        Assert.IsType<SystemTextJsonResult>(response);

    //        var resource = JsonSerializer.Deserialize<Representations.CrawlResultRepresentation>(response.Content);

    //        Assert.Equal("https://devblogs.microsoft.com/dotnet/feed/", resource.Url);
    //        Assert.True(mockPushService.PostWasCalled);
    //    }

    //    private readonly MockAppConfig PushServiceConfig = new MockAppConfig(resourcePush: true,
    //                                                                         resourceApiUrl: "http://domain.com",
    //                                                                         crawlMessageApiUrl: "http://domain.com");

    //    [Fact]
    //    public async void CrawlPushPagetUrl()
    //    {
    //        MockRestService mockPushService = new MockRestService();

    //        var input = MessageBuilder.NewCommand("crawl-request")
    //                                  .WithParameters("uri", "https://blogs.windows.com/windowsexperience/2021/02/23/what-to-know-before-you-accept-that-cookie/")
    //                                  .Make();

    //        CrawlerFunction func = new(PushServiceConfig,
    //                                   new WebResourceCrawler(new RestService(new HttpClient())),
    //                                   mockPushService);

    //        SystemTextJsonResult? response = await func.Crawl(TestFactory.CreateHttpRequest(input),
    //                                                          logger);

    //        Assert.IsType<SystemTextJsonResult>(response);

    //        var resource = JsonSerializer.Deserialize<Representations.CrawlPushedResultRepresentation>(response.Content);

    //        Assert.False(string.IsNullOrEmpty(resource.Display));

    //        Assert.Equal(1,mockPushService.PostCount);
    //        Assert.True(mockPushService.PostWasCalled);
    //    }
        
    //    [Fact]
    //    public async void CrawlPushFeedUrl()
    //    {
    //        MockRestService mockPushService = new MockRestService();

    //        var input = MessageBuilder.NewCommand("crawl-request")
    //                                  .WithParameters("uri", "https://blogs.msdn.microsoft.com/dotnet/feed")
    //                                  .Make();

    //        CrawlerFunction func = new(PushServiceConfig,
    //                                   new WebResourceCrawler(new RestService(new HttpClient())),
    //                                   mockPushService);

    //        SystemTextJsonResult response = await func.Crawl(TestFactory.CreateHttpRequest(input),
    //                                                          logger);

    //        Assert.IsType<SystemTextJsonResult>(response);

    //        var resource = JsonSerializer.Deserialize<Representations.CrawlPushedResultRepresentation>(response.Content);

    //        Assert.False(string.IsNullOrEmpty(resource.Display));

    //        Assert.Equal(11,mockPushService.PostCount);
    //        Assert.True(mockPushService.PostWasCalled);
    //    }
    //}
}