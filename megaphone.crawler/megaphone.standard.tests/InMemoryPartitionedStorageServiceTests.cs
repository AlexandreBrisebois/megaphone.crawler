using Megaphone.Standard.Services;
using System.Threading.Tasks;
using Xunit;

namespace Megaphone.Standard.Tests
{
    public class InMemoryPartitionedStorageServiceTests
    {
        [Fact]
        public void MakeInMemoryStorageService()
        {
            IPartionedStorageService<string> service = new InMemoryPartitionedStorageService<string>();
        }

        [Fact]
        public async Task SetData()
        {
            IPartionedStorageService<string> service = new InMemoryPartitionedStorageService<string>();
            await service.SetAsync("google.com", "content", "<html></html>");
        }

        [Fact]
        public async Task GetData()
        {
            IPartionedStorageService<string> service = new InMemoryPartitionedStorageService<string>();
            await service.SetAsync("google.com", "content", "<html></html>");

            var content = await service.GetAsync("google.com", "content");            
            Assert.True(content == "<html></html>");
        }
    }
}
