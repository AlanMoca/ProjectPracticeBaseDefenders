using System.Threading.Tasks;

namespace Code.Domain.UseCases.Meta.Login
{
    public interface ILoginRequester
    {
        Task Login();
    }
}