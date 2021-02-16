using System.Threading.Tasks;

namespace Megaphone.Standard.Services
{
    public interface IPartionedStorageService<TContent>
    {
        Task<TContent> GetAsync(string partitionKey, string contentKey);
        Task SetAsync(string partitionKey, string contentKey, TContent content);
    }
}

