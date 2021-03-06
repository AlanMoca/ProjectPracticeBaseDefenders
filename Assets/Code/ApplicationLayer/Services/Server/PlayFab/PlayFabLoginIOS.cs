using System.Threading.Tasks;
using PlayFab;
using PlayFab.ClientModels;

namespace Code.ApplicationLayer.Services.Server.PlayFab
{
    public class PlayFabLoginIOS : PlayFabLogin
    {
        protected override void Login( TaskCompletionSource<bool> taskCompletionSource )
        {
            var request = new LoginWithIOSDeviceIDRequest
            {
                DeviceId = UnityEngine.SystemInfo.deviceUniqueIdentifier,
                CreateAccount = true
            };

            PlayFabClientAPI.LoginWithIOSDeviceID( request,
                                                   result => OnSuccess( result, taskCompletionSource ),
                                                   error => OnError( error, taskCompletionSource )
                                                   );
        }
    }
}