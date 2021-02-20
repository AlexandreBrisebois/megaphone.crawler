using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Megaphone.Standard.Services
{
    public class InMemoryPartitionedStorageService<TContent> : IPartionedStorageService<TContent>
    {
        private readonly ConcurrentDictionary<string, ConcurrentDictionary<string, TContent>> storage = new ConcurrentDictionary<string, ConcurrentDictionary<string, TContent>>();

        public Task<TContent> GetAsync(string partitionKey, string contentKey)
        {
            storage.TryAdd(partitionKey, new ConcurrentDictionary<string, TContent>());

            storage.TryGetValue(partitionKey, out var partition);

            if (partition.TryGetValue(contentKey, out var value))
            {
                return Task.FromResult(value);
            }
            throw new KeyNotFoundException(contentKey);
        }

        public Task SetAsync(string partitionKey, string contentKey, TContent content)
        {
            storage.TryAdd(partitionKey, new ConcurrentDictionary<string, TContent>());

            storage.TryGetValue(partitionKey, out var partition);

            partition.AddOrUpdate(contentKey, content, (s, c) => content);

            return Task.CompletedTask;
        }
    }
}

