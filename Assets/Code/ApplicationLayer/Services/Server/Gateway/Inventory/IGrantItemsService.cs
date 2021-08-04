using System.Collections.Generic;
using System.Threading.Tasks;
using PlayFab.ServerModels;

namespace Code.ApplicationLayer.Services.Server.Gateway.Inventory
{
    public interface IGrantItemsService                                                         //Es un servicio que se encarga de darnos las unidades, y si es la primera vez los items iniciales al usuario.
    {
        //Task<List<GrantedItemInstance>> GrantItemsToUsers( string catalogId, List<ItemGrant> itemGrant );
        Task GrantItemsToUsers( string catalogId, List<ItemGrant> itemGrants );
    }
}