using Code.ApplicationLayer.Services.Server.Gateways.Catalog;
using PlayFab;
using PlayFab.ClientModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SystemUtilities;

namespace Code.ApplicationLayer.Services.Server.PlayFab
{
    public class PlayFabCatalogService : ICatalogService                                        //Clase que implementará los métodos que se conectarán directamente con playfab y devolverá los catalogos que pidamos.
    {
        public Task<Optional<List<CatalogItem>>> GetITems( string catalogId )                   //Devolverá el contenido de una lista de un catalogo en especifico de playfab
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
                                              result => OnSuccess( result, taskCompletionSource ),
                                              error => OnError( error, taskCompletionSource )
                                              );
        }

        protected void OnSuccess( GetCatalogItemsResult result, TaskCompletionSource<Optional<List<CatalogItem>>> taskCompletionSource )
        {
            taskCompletionSource.SetResult( new Optional<List<CatalogItem>>( result.Catalog ) );                                   //Así porque queremos que nos regrese el catalogo.
            UnityEngine.Debug.Log( "Ok Units Catalog" );
        }

        protected void OnError( PlayFabError error, TaskCompletionSource<Optional<List<CatalogItem>>> taskCompletionSource )
        {
            throw new Exception( error.GenerateErrorReport() );
        }
    }
}