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

        public InitializeGameUseCase( ILoginRequester loginRequester, IDataPreLoader dataPreLoader )
        {
            this.loginRequester = loginRequester;
            this.dataPreLoader = dataPreLoader;
        }

        public async void InitGame()
        {
            await loginRequester.Login();                                       //Login del usuario
            await dataPreLoader.PreLoad();                                      //Precargamos los datos del usuario y del server, antes de empezar a preguntar por ellos porque esa operaci�n es costosa.                                  //Cargamos datos del server
        }
    }
}
//Este s� es un UseCaseInteractor porque tiene varios useCase y est� trabajando con ellos. La funcionalidad que hace es la de un UseCase, s�lo que a su vez es un interactor (varios use cases).