using Code.ApplicationLayer.Services.Server.DTOs.User;
using Code.ApplicationLayer.Services.Server.Gateways;
using Code.Domain.DataAccess;
using Code.Domain.Entities;
using Code.Domain.Services.Server;
using System.Threading.Tasks;

namespace Code.ApplicationLayer.DataAccess
{
    public class UserRepository : IUserDataAccess
    {
        private readonly IServiceAuthenticator serviceAuthenticator;
        private readonly IGateway userDataGateway;
        private User localUser;

        public UserRepository( IServiceAuthenticator serviceAuthenticator, IGateway userDataGateway )
        {
            this.serviceAuthenticator = serviceAuthenticator;
            this.userDataGateway = userDataGateway;
        }

        public bool IsNewUser()
        {
            var isInitialized = userDataGateway.Contains<IsInitializedDTO>();
            return !isInitialized;
        }

        public User GetLocalUser()
        {
            if( localUser != null )
            {
                return localUser;
            }

            var isInitializedDto = userDataGateway.Contains<IsInitializedDTO>();          //Se usa para verificar si el usuario está inicializado o no.
            localUser = new User( isInitializedDto );
            return localUser;
        }

        public string GetUserId()
        {
            return serviceAuthenticator.UserId;
        }

        public Task CreateLocalUser()
        {
            localUser = new User( true );
            userDataGateway.Set( new IsInitializedDTO( true ) );                                //Guardalo en memoria y marcalo como sucio
            return userDataGateway.Save();
        }
    }
}
