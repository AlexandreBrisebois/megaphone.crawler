using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Megaphone.Crawler.Services
{
    public interface IPushService
    {
        Task<HttpStatusCode> PushAsync(string url, object content);
    }
}
