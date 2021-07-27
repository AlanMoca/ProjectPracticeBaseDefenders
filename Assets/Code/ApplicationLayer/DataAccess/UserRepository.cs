using ApplicationLayer.Services.Server.DTOs.User;
using ApplicationLayer.Services.Server.Gateways;
using Domain.DataAccess;
using Domain.Entities;
using System.Threading.Tasks;

namespace ApplicationLayer.DataAccess
{
    public class UserRepository : IUserDataAccess
    {
        private readonly IGateway userDataGateway;
        private User localUser;

        public UserRepository( IGateway _userDataGateway )
        {
            userDataGateway = _userDataGateway;
        }

        public async Task<User> GetLocalUser()
        {
            if( localUser != null )
            {
                return localUser;
            }

            var isInitializedDto = await userDataGateway.Contains<IsInitializedDTO>();          //Se usa para verificar si el usuario está inicializado o no.
            localUser = new User( isInitializedDto );
            return localUser;
        }
    }
}
