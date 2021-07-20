using System.Threading.Tasks;

namespace Domain.Services.Server
{
    public interface IServiceAuthenticator
    {
        Task Authenticate();
    }
}

