using System.Threading.Tasks;

namespace Megaphone.Crawler.Strategies
{

    internal abstract class Strategy<Model>
    {
        internal abstract bool CanExecute();

        internal abstract Task ExecuteAsync(Model model);
    }

    internal abstract class Strategy<Model, Result>
    {
        internal abstract bool CanExecute();

        internal abstract Task<Result> ExecuteAsync(Model model);
    }
}