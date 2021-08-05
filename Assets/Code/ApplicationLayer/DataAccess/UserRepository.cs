using Code.ApplicationLayer.Services.Server.DTOs.User;
using Code.ApplicationLayer.Services.Server.Gateways;
using Code.Domain.DataAccess;
using Code.Domain.Entities;
using System.Threading.Tasks;

namespace Code.ApplicationLayer.DataAccess
{
    public class UserRepository : IUserDataAccess
    {
        private readonly IGateway userDataGateway;
        private User localUser;

        public UserRepository( IGateway _userDataGateway )
        {
            userDataGateway = _userDataGateway;
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
    }
}
