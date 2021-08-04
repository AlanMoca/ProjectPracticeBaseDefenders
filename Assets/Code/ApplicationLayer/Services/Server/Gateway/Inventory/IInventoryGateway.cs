using PlayFab.ServerModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Code.ApplicationLayer.Services.Server.Gateway.Inventory
{
    public interface IInventoryGateway                                                          //Se encarga de gestionar el inventario del usuario
    {
        Task GrantUnitsToUser( string userId, List<ItemGrant> units );
    }
}