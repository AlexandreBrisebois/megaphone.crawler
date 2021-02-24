using Megaphone.Crawler.Services;
using System.Net;
using System.Threading.Tasks;

namespace Megaphone.Crawler.Tests.Mocks
{
    public class MockPushService : PushService
    {
        public override Task<HttpStatusCode> PushAsync(string url, object content)
        {
            WasCalled = true;
            return Task.FromResult(HttpStatusCode.OK);
        }

        public bool WasCalled { get; set; } = false;
    }
}