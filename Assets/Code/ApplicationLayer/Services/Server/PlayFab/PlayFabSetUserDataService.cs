using System.Collections.Generic;
using System.Threading.Tasks;
using Code.ApplicationLayer.Services.Server.Gateways;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;

namespace Code.ApplicationLayer.Services.Server.PlayFab
{
    public class PlayFabSetUserDataService : ISetDataService
    {
        public Task Set( Dictionary<string, string> data )
        {
            var t = new TaskCompletionSource<bool>();
            UpdateData( data, t );
            return Task.Run( () => t.Task );
        }

        private void UpdateData( Dictionary<string, string> data, TaskCompletionSource<bool> t )
        {
            var request = new UpdateUserDataRequest
            {
                Data = data
            };
            PlayFabClientAPI
                .UpdateUserData( request,
                    result =>
                    {
                        Debug.Log( "UpdateUserData success" );
                        t.SetResult( true );
                    },
                    error =>
                    {
                        Debug.Log(
                            "UpdateUserData error: " + error.GenerateErrorReport() );
                        t.SetCanceled();
                    } );
        }
    }
}