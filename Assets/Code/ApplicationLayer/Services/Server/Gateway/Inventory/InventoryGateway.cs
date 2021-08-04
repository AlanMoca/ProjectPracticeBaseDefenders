using PlayFab.ServerModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Code.ApplicationLayer.Services.Server.Gateway.Inventory
{
    public class InventoryGateway : IInventoryGateway
    {
        private readonly IGrantItemsService grantItemsService;

        public InventoryGateway( IGrantItemsService _grantItemsService )
        {
            grantItemsService = _grantItemsService;
        }

        public async Task GrantUnitsToUser( string userId, List<ItemGrant> units )              //A tal usuario le pasamos esta lista de items.
        {
            await grantItemsService.GrantItemsToUsers( "Units", units );
        }
    }
}