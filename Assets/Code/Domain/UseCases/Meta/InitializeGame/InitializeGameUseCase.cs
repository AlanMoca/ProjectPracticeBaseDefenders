using Code.Domain.UseCases.Meta.LoadServerData;
using Code.Domain.UseCases.Meta.LoadUserData;
using Code.Domain.UseCases.Meta.Login;
using Code.Domain.UseCases.Meta.PreLoadData;

namespace Code.Domain.UseCases.Meta.InitializeGame
{
    public class InitializeGameUseCase : IGameInitializer
    {
        private readonly ILoginRequester loginRequester;
        private readonly IUserDataLoader userDataLoader;
        private readonly IServerDataLoader serverDataLoader;
        private readonly IDataPreLoader dataPreLoader;

        public InitializeGameUseCase( ILoginRequester loginRequester, IUserDataLoader userDataLoader, IServerDataLoader serverDataLoader, IDataPreLoader dataPreLoader )
        {
            this.loginRequester = loginRequester;
            this.userDataLoader = userDataLoader;
            this.serverDataLoader = serverDataLoader;
            this.dataPreLoader = dataPreLoader;
        }

        public async void InitGame()
        {
            await loginRequester.Login();                                       //Login del usuario
            await dataPreLoader.PreLoad();                                                     //Precargamos los datos del usuario y del server, antes de empezar a preguntar por ellos porque esa operación es costosa.
            userDataLoader.Load();                                        //Cargamos datos del usuario
            await serverDataLoader.Load();                                      //Cargamos datos del server
        }
    }
}
//Este sí es un UseCaseInteractor porque tiene varios useCase y está trabajando con ellos. La funcionalidad que hace es la de un UseCase, sólo que a su vez es un interactor (varios use cases).