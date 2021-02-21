using Megaphone.Crawler.Core.Models;
using Megaphone.Crawler.Tests.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Xunit;

namespace Megaphone.Crawler.Tests
{
    public class CrawlerFunctionTests
    {
        private readonly ILogger logger = TestFactory.CreateLogger();

        public static readonly GoodRequestData goodRequestData = new();

        [Theory]
        [MemberData(nameof(goodRequestData))]
        public async void OkRequestTest(object body, string expectedType)
        {
            var response = await CrawlerFunction.Run(TestFactory.CreateHttpRequest(body), logger);

            Assert.IsType<OkObjectResult>(response);
            Assert.IsType<Resource>(response.Value);

            var resource = (Resource)response.Value;

            Assert.Equal(expectedType, resource.Type);
        }

        public static readonly BadRequestData badRequestData = new();

        [Theory]
        [MemberData(nameof(badRequestData))]
        public async void BadRequestTest(object body)
        {
            var response = await CrawlerFunction.Run(TestFactory.CreateHttpRequest(body), logger);

            Assert.IsType<BadRequestObjectResult>(response);
        }
    }
}