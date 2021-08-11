using Code.ApplicationLayer.DataAccess;
using Code.ApplicationLayer.Services.Serializer;
using Code.ApplicationLayer.Services.Server;
using Code.ApplicationLayer.Services.Server.Gateway.Inventory;
using Code.ApplicationLayer.Services.Server.Gateways;
using Code.ApplicationLayer.Services.Server.Gateways.Catalog;
using Code.ApplicationLayer.Services.Server.PlayFab;
using Code.Domain.UseCases.Meta.CreateUser;
using Code.Domain.UseCases.Meta.InitializeGame;
using Code.Domain.UseCases.Meta.LoadServerData;
using Code.Domain.UseCases.Meta.LoadUserData;
using Code.Domain.UseCases.Meta.Login;
using Code.Domain.UseCases.Meta.PreLoadData;
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
            var getUserDataService = new PlayFabGetUserDataService();
            var getTitleDataService = new PlayFabGetTitleDataService();
            var userDataGateway = new UserDataGateway( serializerService, getUserDataService, null );

            //var unitMapper = new UnitMapper( serializerService );
            var userDataAccess = new UserRepository( playFabLogin, userDataGateway );
            var userDataLoader = new LoadUserDataUseCase( userDataAccess );

            var playFabCatalogService = new PlayFabCatalogService();
            var catalogGatewayPlayFab = new CatalogGatewayPlayFab( playFabCatalogService, serializerService );

            var grantItemsServicePlayFab = new PlayFabUserInventoryServices();
            var inventoryGateway = new InventoryGateway( grantItemsServicePlayFab );
            var titleDataGateway = new TitleDataGateway( serializerService, getTitleDataService, null );
            var unitsRepository = new UnitsRepository( titleDataGateway, catalogGatewayPlayFab, inventoryGateway );

            var serverDataLoader = new LoadServerDataUseCase( unitsRepository, playFabLogin );


            
            var dataPreLoaderServiceImpl = new DataPreLoaderService( null, null, null );
            var preLoadDataUseCase = new PreLoadDataUseCase( dataPreLoaderServiceImpl );

            var userInitializer = new InitializeUserUseCase( userDataAccess, unitsRepository );
            var initializeGameUseCase = new InitializeGameUseCase( loginUseCase, preLoadDataUseCase, userInitializer );

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


