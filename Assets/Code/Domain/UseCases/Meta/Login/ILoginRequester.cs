using System.Threading.Tasks;

namespace Domain.UseCases.Meta.Login
{
    public interface ILoginRequester
    {
        Task Login();
    }
}