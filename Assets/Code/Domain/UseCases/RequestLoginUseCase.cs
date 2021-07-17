using System.Threading.Tasks;
using Domain.Services.Server;

namespace Domain.UseCase
{
    public class RequestLoginUseCase : ILoginRequester
    {
        private readonly IAuthenticateService authenticateService;

        public RequestLoginUseCase( IAuthenticateService _authenticateService )
        {
            authenticateService = _authenticateService;
        }

        public async Task Login()
        {
            await authenticateService.Authenticate();
        }
    }
}
