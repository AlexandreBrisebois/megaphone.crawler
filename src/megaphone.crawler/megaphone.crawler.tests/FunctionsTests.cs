using Megaphone.Crawler.Core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Xunit;

namespace megaphone.crawler.tests
{
    public class FunctionsTests
    {
        private readonly ILogger logger = TestFactory.CreateLogger();

        public static readonly GoodRequestData goodRequestData = new();

        [Theory]
        [MemberData(nameof(goodRequestData))]
        public async void OkRequestTest(object body, string expectedType)
        {
            var request = TestFactory.CreateHttpRequest(body);
            var response = await CrawlerFunction.Run(request, logger);

            Assert.IsType<OkObjectResult>(response);
           
            OkObjectResult result = response as OkObjectResult;

            Assert.Equal(expectedType, ((Resource)result.Value).Type);
        }

        public static readonly BadRequestData badRequestData = new();

        [Theory]
        [MemberData(nameof(badRequestData))]
        public async void BadRequestTest(object body)
        {
            var request = TestFactory.CreateHttpRequest(body);
            var response = await CrawlerFunction.Run(request, logger);

            Assert.IsType<BadRequestObjectResult>(response);
        }
    }
}