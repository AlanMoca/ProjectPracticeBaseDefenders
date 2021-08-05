using Code.Domain.Services.Server;
using System.Threading.Tasks;

namespace Code.Domain.UseCases.Meta.PreLoadData
{
    public class PreLoadDataUseCase : IDataPreLoader
    {
        private IDataPreLoaderService serverDataPreLoaderService;
        private IDataPreLoaderService clientDataPreLoaderService;
        private ICatalogDataPreLoaderService catalogDataPreLoaderService;

        public PreLoadDataUseCase( IDataPreLoaderService serverDataPreLoaderService, IDataPreLoaderService clientDataPreLoaderService, ICatalogDataPreLoaderService catalogDataPreLoaderService )
        {
            this.serverDataPreLoaderService = serverDataPreLoaderService;
            this.clientDataPreLoaderService = clientDataPreLoaderService;
            this.catalogDataPreLoaderService = catalogDataPreLoaderService;
        }

        public async Task PreLoad()
        {
            //await serverDataPreLoaderService.PreLoad();   Descomentar
            await clientDataPreLoaderService.PreLoad();
            //await catalogDataPreLoaderService.PreLoad<UnitCustomData>("Units");
        }
    }
}

//NOTA:
//Gran ejemplo de clean code de nombres en la forma en que nombra al usecase y al service. (PreLoadData - DataPreLoader)