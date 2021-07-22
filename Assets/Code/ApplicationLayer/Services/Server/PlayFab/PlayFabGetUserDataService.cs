using ApplicationLayer.Services.Server.Gateways;
using System.Threading.Tasks;
using System.Collections.Generic;
using SystemUtilities;
using PlayFab.ClientModels;
using PlayFab;

namespace ApplicationLayer.Services.Server.PlayFab
{
    public class PlayFabGetUserDataService : IGetDataService                                    //Implementación de playfab
    {
        public Task<Optional<DataResult>> Get( List<string> keys )
        {
            var t = new TaskCompletionSource<Optional<DataResult>>();

            var request = new GetUserDataRequest { Keys = keys };                               //Le preguntamos que Keys son las que quiero
            PlayFabClientAPI.GetUserData( request, 
                                          result => { SetResult( result, t ); }, 
                                          error =>
                                          {
                                              t.SetResult( new Optional<DataResult>() );        //Creo sólo creo un nuevo diccionario
                                          }
                                          );

            return Task.Run( () => t.Task );
        }

        private void SetResult( GetUserDataResult result, TaskCompletionSource<Optional<DataResult>> t )    //Lo que hago es copiar creo el diccionario que playfab regresa (result.data) y devolviendolo.
        {
            var resultData = new Dictionary<string, string>( result.Data.Count );               
            foreach( var userDataRecord in result.Data )
            {
                resultData.Add( userDataRecord.Key, userDataRecord.Value.Value );
            }

            t.SetResult( new Optional<DataResult>( new DataResult( resultData ) ) );
        }
    }
}
    
