using Code.Domain.Services.Server;
using System.Threading.Tasks;

namespace Code.Domain.UseCases.Meta.PreLoadData
{
    public class PreLoadDataUseCase : IDataPreLoader
    {
        private IDataPreLoaderService dataPreLoaderService;

        public PreLoadDataUseCase( IDataPreLoaderService dataPreLoaderService )
        {
            this.dataPreLoaderService = dataPreLoaderService;
        }

        public async Task PreLoad()
        {
            await dataPreLoaderService.PreLoad();
        }
    }
}

//NOTA:
//Gran ejemplo de clean code de nombres en la forma en que nombra al usecase y al service. (PreLoadData - DataPreLoader)