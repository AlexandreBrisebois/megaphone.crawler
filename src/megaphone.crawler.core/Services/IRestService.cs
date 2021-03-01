using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Megaphone.Crawler.Core.Services
{
    public interface IRestService
    {
        Task<HttpStatusCode> PostAsync(string url, object content);
        Task<HttpResponseMessage> GetAsync(string url);
    }
}
