using Megaphone.Crawler.Core.Models;
using System;
using System.Threading.Tasks;

namespace Megaphone.Crawler.Core
{
    public interface IWebResourceCrawler
    {
        Task<Resource> GetResourceAsync(string uri);
    }
}