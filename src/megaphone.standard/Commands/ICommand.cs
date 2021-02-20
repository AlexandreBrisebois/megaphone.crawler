using System.Threading.Tasks;

namespace Megaphone.Standard.Commands
{
    public interface ICommand<in TModel>
    {
        Task ApplyAsync(TModel model);
    }
}