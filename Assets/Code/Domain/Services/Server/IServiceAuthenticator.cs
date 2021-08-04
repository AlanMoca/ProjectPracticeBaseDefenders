using System.Threading.Tasks;

namespace Code.Domain.Services.Server
{
    public interface IServiceAuthenticator
    {
        public string UserId { get; }
        Task Authenticate();
    }
}

