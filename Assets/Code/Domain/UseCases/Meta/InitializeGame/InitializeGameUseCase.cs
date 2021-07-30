using Domain.UseCases.Meta.LoadServerData;
using Domain.UseCases.Meta.LoadUserData;
using Domain.UseCases.Meta.Login;

namespace Domain.UseCases.Meta.InitializeGame
{
    public class InitializeGameUseCase : IGameInitializer
    {
        private readonly ILoginRequester loginRequester;
        private readonly IUserDataLoader userDataLoader;
        private readonly IServerDataLoader serverDataLoader;

        public InitializeGameUseCase( ILoginRequester _loginRequester, IUserDataLoader _userDataLoader, IServerDataLoader _serverDataLoader )
        {
            loginRequester = _loginRequester;
            userDataLoader = _userDataLoader;
            serverDataLoader = _serverDataLoader;
        }

        public async void InitGame()
        {
            await loginRequester.Login();
            await serverDataLoader.Load();
            await userDataLoader.Load();
        }
    }
}
//Este s� es un UseCaseInteractor porque tiene varios useCase y est� trabajando con ellos. La funcionalidad que hace es la de un UseCase, s�lo que a su vez es un interactor (varios use cases).