using System.Threading.Tasks;

namespace Domain.Services.Server
{
    public interface IAuthenticateService
    {
        Task Authenticate();
    }
}

