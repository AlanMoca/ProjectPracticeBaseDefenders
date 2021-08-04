using Code.Domain.DataAccess;
using Code.ApplicationLayer.Services.Server.DTOs.Server;
using Code.ApplicationLayer.Services.Server.Gateways.Catalog;
using Code.Domain.Entities;
using PlayFab.ServerModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Code.ApplicationLayer.Services.Server.Gateway.Inventory;
using ApplicationLayer.DataAccess;

namespace Code.ApplicationLayer.DataAccess
{
    public class UnitsRepository : IUnitsDataAccess                                             //Clase que se encarga de mappear los datos DTO a entidades
    {
        private readonly ICatalogGateway _catalogGateway;
        private readonly IInventoryGateway _inventoryGateway;
        private List<Unit> _units;

        public UnitsRepository( ICatalogGateway catalogGateway, IInventoryGateway inventoryGateway )
        {
            _catalogGateway = catalogGateway;
            _inventoryGateway = inventoryGateway;
        }

        public async Task<IReadOnlyList<Unit>> GetAllUnits()
        {
            var unitsDTOs = await _catalogGateway.GetItems<UnitCustomDataDTO>( "Units" );       //Este es el item que le estás pidiendo al gateway.

            var unitMapper = new UnitMapper();                                                  //El mappeado lo extrajimo a otra clase para quitarle esta responsabilidad al repository. Aunque el patrón Repository
            _units = new List<Unit>( unitsDTOs.Select( unitMapper.ParseUnitsDTO ) );            //contempla hacer estos mapeos, no quiere decir que los tenga que hacer él necesariamente, puede usar un colaborador como aquí.

            return _units;
        }

        public async Task AddUnitsToUser( string userId, List<UnitToAdd> units )                //Añade estas unidades al usuario
        {
            var items = 
                new List<ItemGrant>( units.
                                        Select( unit => new ItemGrant
                                                        {
                                                            ItemId = unit.Id,
                                                            Data = ParseFieldsToDictionary( unit ),
                                                            PlayFabId = userId
                                                        }
                                        )
                                    );

            await _inventoryGateway.GrantUnitsToUser( userId, items );
        }

        private Dictionary<string, string> ParseFieldsToDictionary( UnitToAdd unit )            //Esto se hizo por reflexión porque aunque no es optima, permite hacer un código genérico.
        {
            var type = unit.UnitState.GetType();
            var fieldInfos = type.GetFields( System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic );
            return fieldInfos.ToDictionary( field => field.Name,
                                            field => field.GetValue( unit.UnitState ).ToString() 
                                            );
        }
    }
}