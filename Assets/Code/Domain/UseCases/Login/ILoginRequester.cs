using System.Threading.Tasks;

namespace Domain.UseCases
{
    public interface ILoginRequester
    {
        Task Login();
    }
}