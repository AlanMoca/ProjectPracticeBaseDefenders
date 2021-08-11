using Code.Domain.UseCases.Meta.CreateUser;
using Code.Domain.UseCases.Meta.LoadServerData;
using Code.Domain.UseCases.Meta.LoadUserData;
using Code.Domain.UseCases.Meta.Login;
using Code.Domain.UseCases.Meta.PreLoadData;

namespace Code.Domain.UseCases.Meta.InitializeGame
{
    public class InitializeGameUseCase : IGameInitializer
    {
        private readonly ILoginRequester loginRequester;
        private readonly IDataPreLoader dataPreLoader;
        private readonly IUserInitializer userInitializer;

        public InitializeGameUseCase( ILoginRequester loginRequester, IDataPreLoader dataPreLoader, IUserInitializer userInitializer )
        {
            this.loginRequester = loginRequester;
            this.dataPreLoader = dataPreLoader;
            this.userInitializer = userInitializer;
        }

        public async void InitGame()
        {
            await loginRequester.Login();                                       //Login del usuario
            await dataPreLoader.PreLoad();                                      //Precargamos los datos del usuario y del server, antes de empezar a preguntar por ellos porque esa operación es costosa.
            userInitializer.Init();
        }
    }
}
//Este sí es un UseCaseInteractor porque tiene varios useCase y está trabajando con ellos. La funcionalidad que hace es la de un UseCase, sólo que a su vez es un interactor (varios use cases).