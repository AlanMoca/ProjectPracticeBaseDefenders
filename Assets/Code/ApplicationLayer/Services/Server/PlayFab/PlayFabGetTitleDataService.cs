using Code.ApplicationLayer.Services.Server.Gateways;
using PlayFab;
using PlayFab.ClientModels;
using System.Collections.Generic;
using System.Threading.Tasks;
using SystemUtilities;
using UnityEngine;

//No sé en qué momento se hizo esta clase D:
namespace Code.ApplicationLayer.Services.Server.PlayFab
{
    public class PlayFabGetTitleDataService : IGetDataService
    {
        public Task<Optional<DataResult>> Get( List<string> keys )
        {
            var t = new TaskCompletionSource<Optional<DataResult>>();

            GetTitleData( keys, t );

            return Task.Run( () => t.Task );
        }

        private static void GetTitleData( List<string> keys, TaskCompletionSource<Optional<DataResult>> t )
        {
            var request = new GetTitleDataRequest { Keys = keys };

            PlayFabClientAPI
                .GetTitleData( request,
                    result =>
                    {
                        Debug.Log( "GetTitleData success" );
                        t.SetResult( new Optional<DataResult>( new DataResult( result.Data ) ) );
                    },
                    error =>
                    {
                        Debug.Log( "GetTitleData error: " + error.GenerateErrorReport() );
                        t.SetResult( new Optional<DataResult>() );
                    } );
        }
    }
}