using Megaphone.Crawler.Core.Models;
using Newtonsoft.Json;
using System.Net;
using Xunit;

namespace megaphone.crawler.tests
{
    public class FunctionsTests
    {

        public static readonly GoodRequestData goodRequestData = new();

        [Theory]
        [MemberData(nameof(goodRequestData))]
        public async void OkRequestTest(object body, string expectedType)
        {
            var request = RequestBuilder.CreatePostHttpRequest(body);
            var response = await CrawlerFunction.Run(request);

            var resource = JsonConvert.DeserializeObject<Resource>(response.Body);
    
            Assert.Equal(expectedType, resource.Type);
        }

        public static readonly BadRequestData badRequestData = new();

        [Theory]
        [MemberData(nameof(badRequestData))]
        public async void BadRequestTest(object body)
        {
            var request = RequestBuilder.CreatePostHttpRequest(body);
            var response = await CrawlerFunction.Run(request);

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
    }
}