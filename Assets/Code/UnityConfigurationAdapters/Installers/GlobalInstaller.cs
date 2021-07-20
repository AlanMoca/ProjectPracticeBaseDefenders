using ApplicationLayer.Services.Server;
using Domain.UseCase;
using UnityEngine;

namespace Code.UnityConfigurationAdapters.Installers
{
    public class GlobalInstaller : MonoBehaviour
    {
        private void Awake()
        {
            PlayFabLogin playFabLogin = GetPlayFabLogin();
            RequestLoginUseCase loginUseCase = new RequestLoginUseCase( playFabLogin );
            InitializeGameUseCase initializeGameUseCase = new InitializeGameUseCase( loginUseCase );
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


