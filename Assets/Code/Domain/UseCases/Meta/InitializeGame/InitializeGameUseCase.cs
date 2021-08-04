using Code.Domain.UseCases.Meta.LoadServerData;
using Code.Domain.UseCases.Meta.LoadUserData;
using Code.Domain.UseCases.Meta.Login;

namespace Code.Domain.UseCases.Meta.InitializeGame
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
//Este sí es un UseCaseInteractor porque tiene varios useCase y está trabajando con ellos. La funcionalidad que hace es la de un UseCase, sólo que a su vez es un interactor (varios use cases).