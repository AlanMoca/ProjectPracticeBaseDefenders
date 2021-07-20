using System.Threading.Tasks;

namespace Domain.UseCase
{
    public interface ILoginRequester
    {
        Task Login();
    }
}