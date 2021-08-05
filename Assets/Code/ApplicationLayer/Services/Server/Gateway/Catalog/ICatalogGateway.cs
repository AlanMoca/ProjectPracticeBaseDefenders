using Code.ApplicationLayer.Services.Server.DTOs.Server;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Code.ApplicationLayer.Services.Server.Gateways.Catalog
{
    public interface ICatalogGateway
    {
        IReadOnlyList<CatalogItemDTO> GetItems( string catalogId );
    }
}

//NOTA: Nos devuelve una collection "IReadOnlyList" que en este caso es una lista de s�lo lectura porque no queremos que nadie pueda a�adir o modificar elementos y si lo hacen nos queremos enterar para subirlos tambi�n al servidor.

