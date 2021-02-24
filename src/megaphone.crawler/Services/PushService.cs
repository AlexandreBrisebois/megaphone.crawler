using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Megaphone.Crawler.Services
{
    public abstract class PushService
    {
        public abstract Task<HttpStatusCode> PushAsync(string url, object content);
    }
}
