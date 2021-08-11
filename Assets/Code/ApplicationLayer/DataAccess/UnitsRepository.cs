using Code.Domain.DataAccess;
using Code.ApplicationLayer.Services.Server.Gateways.Catalog;
using Code.Domain.Entities;
using PlayFab.ServerModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Code.ApplicationLayer.Services.Server.Gateway.Inventory;
using Code.ApplicationLayer.Services.Server.Gateways;
using Code.ApplicationLayer.Services.Server.DTOs.Server;

namespace Code.ApplicationLayer.DataAccess
{
    public class UnitsRepository : IUnitsDataAccess                                             //Clase que se encarga de mappear los datos DTO a entidades
    {
        private readonly Gateway _titleDataGateway;
        private readonly ICatalogGateway _catalogGateway;
        private readonly IInventoryGateway _inventoryGateway;
        private List<Unit> _units;

        public UnitsRepository( Gateway titleDataGateway, ICatalogGateway catalogGateway, IInventoryGateway inventoryGateway )
        {
            _titleDataGateway = titleDataGateway;
            _catalogGateway = catalogGateway;
            _inventoryGateway = inventoryGateway;
        }

        public IReadOnlyList<Unit> GetAllUnits()
        {
            var unitsDTOs = _catalogGateway.GetItems( "Units" );       //Este es el item que le estás pidiendo al gateway.

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

        public IReadOnlyList<string> GetInitialUnitsId()
        {
            var initialUnitsDTO = _titleDataGateway.Get<InitialUnitsDTO>();
            return initialUnitsDTO.UnitsId;                                                     //Con esto ya estamos obteniendo las unidades iniciales (configuración del servidor)
        }

        private Dictionary<string, string> ParseFieldsToDictionary( UnitToAdd unit )            //Esto se hizo por reflexión porque aunque no es optima ya que tiene que analizar como está construida la clase, pero permite hacer un código genérico y no la ejecutaremos cada clic sólo cuando añadamos unidades.
        {
            var type = unit.UnitState.GetType();                                                                                        //Guardo el tipo UnitState
            var fieldInfos = type.GetFields( System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic );      //A este tipo dame todos campos que sean parte de la instancias o sea no estáticos y no publicos.
            return fieldInfos.ToDictionary( field => field.Name,                                                                        //Esos fields los convierto en un diccionario 
                                            field => field.GetValue( unit.UnitState ).ToString()                                        //El valor lo convierto a string porque playfab trabaja string, string :( (hubiera sido mejor json)
                                            );
        }
    }
}