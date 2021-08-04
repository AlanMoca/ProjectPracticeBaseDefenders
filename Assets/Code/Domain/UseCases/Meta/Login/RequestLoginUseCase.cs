using System.Threading.Tasks;
using Code.Domain.Services.Server;

namespace Code.Domain.UseCases.Meta.Login
{
    public class RequestLoginUseCase : ILoginRequester
    {
        private readonly IServiceAuthenticator serviceAuthenticator;

        public RequestLoginUseCase( IServiceAuthenticator _authenticateService )
        {
            serviceAuthenticator = _authenticateService;
        }

        public async Task Login()
        {
            await serviceAuthenticator.Authenticate();
        }
    }
}
