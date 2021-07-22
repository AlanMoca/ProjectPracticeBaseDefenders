using System.Threading.Tasks;

namespace Domain.UseCases
{
    public interface IUserDataLoader
    {
        Task Load();
    }
}