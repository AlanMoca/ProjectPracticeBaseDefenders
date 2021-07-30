using ApplicationLayer.DataAccess;
using ApplicationLayer.Services.Serializer;
using ApplicationLayer.Services.Server.Gateways;
using ApplicationLayer.Services.Server.Gateways.Catalog;
using ApplicationLayer.Services.Server.PlayFab;
using Domain.UseCases.Meta.InitializeGame;
using Domain.UseCases.Meta.LoadServerData;
using Domain.UseCases.Meta.LoadUserData;
using Domain.UseCases.Meta.Login;
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

            var playFabCatalogService = new PlayFabCatalogService();
            
            var catalogGatewayPlayFab = new CatalogGatewayPlayFab( playFabCatalogService, serializerService );
            var unitsRepository = new UnitsRepository( catalogGatewayPlayFab );
            var serverDataLoader = new LoadServerDataUseCase( unitsRepository );

            var initializeGameUseCase = new InitializeGameUseCase( loginUseCase, userDataLoader, serverDataLoader );

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


