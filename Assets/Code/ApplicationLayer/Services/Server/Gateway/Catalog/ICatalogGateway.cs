using ApplicationLayer.Services.Server.DTOs.Server;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApplicationLayer.Services.Server.Gateways.Catalog
{
    public interface ICatalogGateway
    {
        Task<IReadOnlyList<CatalogItemDTO>> GetItems<T>( string catalogId );                    //Nos devuelve una collection "IReadOnlyList" que en este caso es una lista de s�lo lectura porque no queremos que nadie pueda a�adir o modificar elementos, no con la lista.
    }
}
