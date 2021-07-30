using ApplicationLayer.Services.Server.DTOs.Server;
using ApplicationLayer.Services.Server.Gateways.Catalog;
using Domain.DataAccess;
using Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApplicationLayer.DataAccess
{
    public class UnitsRepository : IUnitsDataAccess                                             //Clase que se encarga de mappear los datos DTO a entidades
    {
        private readonly ICatalogGateway _catalogGateway;
        private List<Unit> _units;

        public UnitsRepository( ICatalogGateway catalogGateway )
        {
            _catalogGateway = catalogGateway;
        }

        public async Task<IReadOnlyList<Unit>> GetAllUnits()
        {
            var unitsDTOs = await _catalogGateway.GetItems<UnitCustomDataDTO>( "Units" );       //Este es el item que le estás pidiendo al gateway.
            _units = new List<Unit>( unitsDTOs.Select( unitDTO =>
            {
                var unitCustomData = unitDTO.GetCustomData<UnitCustomDataDTO>();
                return new Unit( unitDTO.ID, unitDTO.DisplayName, unitCustomData.Attack, unitCustomData.Health );
            } ) );
            return _units;
        }

        ////////////////////////Aquí abajo estoy refactorizando

        //Los parseos posiblemente los extraigamos en otra clase que sea como un mappeador que sólo se encargue de eso, de esta forma le extraemos esta responsabilidad que no le corresponde tanto al repository.
        //Aunque el patrón Repository contempla hacer estos mapeos, no quiere decir que los tenga que hacer él necesariamente, puede usar un colaborador.

        private Unit ParseUnits( CatalogItemDTO unitDTO )
        {
            return ParseUnit( unitDTO );
        }

        private Unit ParseUnit( CatalogItemDTO unitDTO )
        {
            var unitCustomData = unitDTO.GetCustomData<UnitCustomDataDTO>();
            var unit = new Unit( unitDTO.ID,
                                 unitDTO.DisplayName,
                                 unitCustomData.Attack,
                                 unitCustomData.Health );
            return unit;
        }

    }
}
//_units = new List<Unit>( unitsDTOs.Select( unitDTO => new Unit( unitDTO.ID, unitDTO.DisplayName, unitDTO.GetCustomData<UnitCustomDataDTO> ) ) );
//NOTA: Una entidad no puede conocer un DTO, por lo que no le podemos pasar el DTO como tal entonces pasamos los parametros.
//Entonces queda: