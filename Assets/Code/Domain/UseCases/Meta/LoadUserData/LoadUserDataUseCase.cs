using Code.Domain.DataAccess;
using System.Threading.Tasks;

namespace Code.Domain.UseCases.Meta.LoadUserData
{
    public class LoadUserDataUseCase : IUserDataLoader
    {
        private readonly IUserDataAccess userDataAccess;

        public LoadUserDataUseCase( IUserDataAccess _userDataAccess )
        {
            userDataAccess = _userDataAccess;
        }

        public void Load()
        {
            var localUser = userDataAccess.GetLocalUser();
            UnityEngine.Debug.Log( localUser.isInitialized );
        }
    }
}