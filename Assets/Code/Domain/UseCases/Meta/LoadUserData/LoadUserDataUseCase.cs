using Domain.DataAccess;
using System.Threading.Tasks;

namespace Domain.UseCases.Meta.LoadUserData
{
    public class LoadUserDataUseCase : IUserDataLoader
    {
        private readonly IUserDataAccess userDataAccess;

        public LoadUserDataUseCase( IUserDataAccess _userDataAccess )
        {
            userDataAccess = _userDataAccess;
        }

        public async Task Load()
        {
            var localUser = await userDataAccess.GetLocalUser();
            UnityEngine.Debug.Log( localUser.isInitialized );
        }
    }
}