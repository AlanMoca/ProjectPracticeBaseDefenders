using Code.ApplicationLayer.Services.Server.DTOs.Server;
using Code.Domain.Services.Server;
using System.Threading.Tasks;

namespace Code.ApplicationLayer.Services.Server
{
    public class DataPreLoaderService : IDataPreLoaderService                                   //Al usarse como interface y variable es un patrón de diseño pero no recuerdo cual creo mediator
    {
        private readonly IDataPreLoaderService serverDataPreLoaderService;
        private readonly IDataPreLoaderService clientDataPreLoaderService;
        private readonly ICatalogDataPreLoaderService catalogDataPreLoaderService;

        public DataPreLoaderService( IDataPreLoaderService serverDataPreLoaderService, 
                                     IDataPreLoaderService clientDataPreLoaderService, 
                                     ICatalogDataPreLoaderService catalogDataPreLoaderService )
        {
            this.serverDataPreLoaderService = serverDataPreLoaderService;
            this.clientDataPreLoaderService = clientDataPreLoaderService;
            this.catalogDataPreLoaderService = catalogDataPreLoaderService;
        }

        public async Task PreLoad()
        {
            await serverDataPreLoaderService.PreLoad();
            await clientDataPreLoaderService.PreLoad();
            await catalogDataPreLoaderService.PreLoad<UnitCustomDataDTO>( "Units" );
        }
    }
}