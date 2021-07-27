using ApplicationLayer.Services.Server.Gateways.Catalog;
using PlayFab;
using PlayFab.ClientModels;
using System.Collections.Generic;
using System.Threading.Tasks;
using SystemUtilities;

namespace ApplicationLayer.Services.Server.PlayFab
{
    public class PlayFabCatalogService : ICatalogService                                        //Clase que implementar� los m�todos que se conectar�n directamente con playfab y devolver� los catalogos que pidamos.
    {
        public Task<Optional<List<CatalogItem>>> GetITems( string catalogId )                   //Devolver� el contenido de una lista de un catalogo en especifico de playfab
        {
            var taskCompletionSource = new TaskCompletionSource<Optional<List<CatalogItem>>>();

            GetCatalogItems( catalogId, taskCompletionSource );

            return Task.Run( () => taskCompletionSource.Task );
        }

        private void GetCatalogItems( string catalogId, TaskCompletionSource<Optional<List<CatalogItem>>> taskCompletionSource )
        {
            var request = new GetCatalogItemsRequest
            {
                CatalogVersion = catalogId
            };

            PlayFabClientAPI.GetCatalogItems( request,
                                              result => OnSuccess( result, t ),
                                              error => OnError( error, t )
                                              );
        }

        protected void OnSuccess( GetCatalogItemsResult result, TaskCompletionSource<Optional<List<CatalogItem>>> taskCompletionSource )
        {
            taskCompletionSource.SetResult( result.Catalog );                                   //As� porque queremos que nos regrese el catalogo.
            UnityEngine.Debug.Log( "Ok Units Catalog" );
        }

        protected void OnError( PlayFabError error, TaskCompletionSource<Optional<List<CatalogItem>>> taskCompletionSource )
        {
            throw new Exception( error.GenerateErrorReport() );
        }
    }
}