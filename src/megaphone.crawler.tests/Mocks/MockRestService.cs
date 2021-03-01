using Megaphone.Crawler.Core.Services;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Megaphone.Crawler.Tests.Mocks
{
    public class MockRestService : IRestService
    {
        public int PostCount { get; private set; } = 0;
        public int GetCount { get; private set; } = 0;

        public Task<HttpStatusCode> PostAsync(string url, object content)
        {
            PostCount++;
            PostWasCalled = true;
            return Task.FromResult(HttpStatusCode.Created);
        }

        public Task<HttpResponseMessage> GetAsync(string url)
        {
            GetWasCalled = true;
            GetCount++;

            return Task.FromResult(new HttpResponseMessage());
        }

        public bool PostWasCalled { get; set; } = false;
        public bool GetWasCalled { get; set; } = false;
    }
}