using Megaphone.Crawler.Services;
using System.Net;
using System.Threading.Tasks;

namespace Megaphone.Crawler.Tests.Mocks
{
    public class MockPushService : IPushService
    {
        public Task<HttpStatusCode> PushAsync(string url, object content)
        {
            WasCalled = true;
            return Task.FromResult(HttpStatusCode.Created);
        }

        public bool WasCalled { get; set; } = false;
    }
}