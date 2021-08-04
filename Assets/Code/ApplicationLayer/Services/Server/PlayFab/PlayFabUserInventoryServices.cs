using Code.ApplicationLayer.Services.Server.Gateway.Inventory;
using PlayFab;
using PlayFab.ServerModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Code.ApplicationLayer.Services.Server.PlayFab
{
    public class PlayFabUserInventoryServices : IGrantItemsService
    {
        public Task GrantItemsToUsers( string catalogId, List<ItemGrant> itemGrants )
        {
            var taskCompletionSource = new TaskCompletionSource<bool>();
            GrantItems( catalogId, itemGrants, taskCompletionSource );
            return Task.Run( () => taskCompletionSource.Task );
        }

        private void GrantItems( string catalogId, List<ItemGrant> itemGrants,
                                 TaskCompletionSource<bool> taskCompletionSource )
        {
            var request = new GrantItemsToUsersRequest
            {
                CatalogVersion = catalogId,
                ItemGrants = itemGrants
            };

            PlayFabServerAPI.                                                                   //Es necesario recordar que para usar la API de SERVER, tenemos que configurar la directiva de precompilado en unity además de tener activada la casilla en los settings del sdk de playfab dentro del mismo unity.
                GrantItemsToUsers( request,                                                     //La llamada se hace con la API de SERVER, porque en la api de cliente no podemos darle items al usuario.
                                   result => OnSucess( result, taskCompletionSource ),
                                   error => OnError( error, taskCompletionSource )
                                   );
        }

        private void OnSucess( GrantItemsToUsersResult result, TaskCompletionSource<bool> taskCompletionSource )
        {
            taskCompletionSource.SetResult( true );
            UnityEngine.Debug.Log( "Grant Items succesfully" );
        }

        private void OnError( PlayFabError error, TaskCompletionSource<bool> taskCompletionSource )
        {
            taskCompletionSource.SetResult( false );
            UnityEngine.Debug.Log( "Grant Items" );
            throw new NotImplementedException();
        }
    }
}