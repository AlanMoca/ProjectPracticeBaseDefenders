using System.Threading.Tasks;
using PlayFab;
using PlayFab.ClientModels;

namespace Code.ApplicationLayer.Services.Server.PlayFab
{
    public class PlayFabLoginAndroid : PlayFabLogin
    {
        protected override void Login( TaskCompletionSource<bool> taskCompletionSource )
        {
            var request = new LoginWithAndroidDeviceIDRequest
            {
                AndroidDeviceId = UnityEngine.SystemInfo.deviceUniqueIdentifier,
                CreateAccount = true
            };

            PlayFabClientAPI.LoginWithAndroidDeviceID( request,
                                                       result => OnSuccess( result, taskCompletionSource ),
                                                       error => OnError( error, taskCompletionSource )
                                                       );
        }
    }
}