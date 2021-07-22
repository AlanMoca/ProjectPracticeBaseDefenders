namespace Domain.UseCases
{
    public class InitializeGameUseCase : IGameInitializer
    {
        private readonly ILoginRequester loginRequester;
        private readonly IUserDataLoader userDataLoader;

        public InitializeGameUseCase( ILoginRequester _loginRequester, IUserDataLoader _userDataLoader )
        {
            loginRequester = _loginRequester;
            userDataLoader = _userDataLoader;
        }

        public async void InitGame()
        {
            await loginRequester.Login();
            //Load server configuration
            await userDataLoader.Load();
        }
    }
}