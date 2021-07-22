using ApplicationLayer.DataAccess;
using ApplicationLayer.Services.Serializer;
using ApplicationLayer.Services.Server.Gateways;
using ApplicationLayer.Services.Server.PlayFab;
using Domain.UseCases;
using UnityEngine;

namespace Code.UnityConfigurationAdapters.Installers
{
    public class GlobalInstaller : MonoBehaviour
    {
        private void Awake()
        {
            var playFabLogin = GetPlayFabLogin();
            var loginUseCase = new RequestLoginUseCase( playFabLogin );

            var serializerService = new UnityJsonSerializer();
            var getDataService = new PlayFabGetUserDataService();
            var userDataGateway = new UserDataGateway( serializerService, getDataService, null );

            var userDataAccess = new UserRepository( userDataGateway );
            var userDataLoader = new LoadUserDataUseCase( userDataAccess );

            var initializeGameUseCase = new InitializeGameUseCase( loginUseCase, userDataLoader );

            initializeGameUseCase.InitGame();
        }

        private PlayFabLogin GetPlayFabLogin()
        {
#if UNITY_EDITOR
            return new PlayFabLoginEditor();
#elif UNITY_ANDROID
            return new PlayFabLoginAndroid();
#elif UNITY_IOS
            return new PlayFabLoginIOS();
#endif
        }
    }
}


