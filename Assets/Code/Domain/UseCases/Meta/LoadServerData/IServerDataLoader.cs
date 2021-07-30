using System.Threading.Tasks;

namespace Domain.UseCases.Meta.LoadServerData
{
    public interface IServerDataLoader
    {
        Task Load();
    }
}