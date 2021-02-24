using Megaphone.Crawler.Core.Models;
using System.Threading.Tasks;

namespace Megaphone.Crawler.Strategies
{
    internal abstract class ResponseStrategy<T>
    {
        protected readonly AppConfig serviceContext;

        public ResponseStrategy(AppConfig serviceContext)
        {
            this.serviceContext = serviceContext;
        }

        internal abstract bool CanExecute();

        internal abstract Task<T> ExecuteAsync(Resource resource);
    }
}