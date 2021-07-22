using System.Threading.Tasks;
using Domain.Services.Server;

namespace Domain.UseCases
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
