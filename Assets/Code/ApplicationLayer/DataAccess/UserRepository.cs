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

        public UserRepository( IGateway _userDataGateway )
        {
            userDataGateway = _userDataGateway;
        }

        public async Task<User> GetLocalUser()
        {
            var isInitializedDto = await userDataGateway.Contains<IsInitializedDTO>();          //Se usa para verificar si el usuario está inicializado o no.
            return new User( isInitializedDto );
        }
    }
}
