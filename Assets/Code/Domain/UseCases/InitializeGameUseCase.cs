namespace Domain.UseCase
{
    public class InitializeGameUseCase : IGameInitializer
    {
        private readonly ILoginRequester loginRequester;

        public InitializeGameUseCase( ILoginRequester _loginRequester )
        {
            loginRequester = _loginRequester;
        }

        public async void InitGame()
        {
            await loginRequester.Login();
            //Load server configuration
            //Load user configuration
        }
    }
}