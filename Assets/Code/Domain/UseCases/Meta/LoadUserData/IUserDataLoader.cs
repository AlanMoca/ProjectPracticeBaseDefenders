using System.Threading.Tasks;

namespace Code.Domain.UseCases.Meta.LoadUserData
{
    public interface IUserDataLoader
    {
        Task Load();
    }
}