using Megaphone.Crawler.Core.Models;
using System.Threading.Tasks;

namespace Megaphone.Crawler.Core.Services
{
    public interface IResourceService
    {
        Task<ResourceStatus> GetStatusAsync(string uri);
        Task PostAsync(Resource resource);
    }
}
