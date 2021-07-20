using ApplicationLayer.Services.Server;
using Domain.UseCase;
using UnityEngine;

namespace Code.UnityConfigurationAdapters.Installers
{
    public class GlobalInstaller : MonoBehaviour
    {
        private void Awake()
        {
            PlayFabLogin playFabLogin = new PlayFabLogin();
            RequestLoginUseCase loginUseCase = new RequestLoginUseCase( playFabLogin );
            InitializeGameUseCase initializeGameUseCase = new InitializeGameUseCase( loginUseCase );

            initializeGameUseCase.InitGame();
        }
    }
}


