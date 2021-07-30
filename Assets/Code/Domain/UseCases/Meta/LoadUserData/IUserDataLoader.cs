using System.Threading.Tasks;

namespace Domain.UseCases.Meta.LoadUserData
{
    public interface IUserDataLoader
    {
        Task Load();
    }
}