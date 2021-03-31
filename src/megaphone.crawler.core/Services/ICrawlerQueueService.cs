using Megaphone.Standard.Messages;
using System.Threading.Tasks;

namespace Megaphone.Crawler.Core.Services
{
    public interface ICrawlerQueueService
    {
        Task Enqueue(CommandMessage commandMessage);
    }
}
