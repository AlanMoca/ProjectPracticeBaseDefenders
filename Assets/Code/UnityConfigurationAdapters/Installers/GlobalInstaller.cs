using Code.ApplicationLayer.DataAccess;
using Code.ApplicationLayer.Services.Serializer;
using Code.ApplicationLayer.Services.Server.Gateway.Inventory;
using Code.ApplicationLayer.Services.Server.Gateways;
using Code.ApplicationLayer.Services.Server.Gateways.Catalog;
using Code.ApplicationLayer.Services.Server.PlayFab;
using Code.Domain.UseCases.Meta.InitializeGame;
using Code.Domain.UseCases.Meta.LoadServerData;
using Code.Domain.UseCases.Meta.LoadUserData;
using Code.Domain.UseCases.Meta.Login;
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

            var grantItemsService = new PlayFabUserInventoryServices();
            var inventoryGateway = new InventoryGateway( grantItemsService );
            var unitsRepository = new UnitsRepository( catalogGatewayPlayFab, inventoryGateway );

            var serverDataLoader = new LoadServerDataUseCase( unitsRepository, playFabLogin );

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


