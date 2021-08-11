using Code.ApplicationLayer.Services.Server.DTOs.Server;
using Code.Domain.Entities;

namespace Code.ApplicationLayer.DataAccess
{
    public class UnitMapper
    {
        public Unit ParseUnitsDTO( CatalogItemDTO unitDTO )
        {
            return ParseUnitDTO( unitDTO );
        }

        private Unit ParseUnitDTO( CatalogItemDTO unitDTO )
        {
            var unitCustomData = unitDTO.GetCustomData<UnitCustomDataDTO>();

            var unit = new Unit( unitDTO.ID,                                                    //Una entidad no puede conocer un DTO, por lo que no le podemos pasar el DTO como tal entonces pasamos los parametros.
                                 unitDTO.DisplayName,
                                 unitCustomData.UnitAttributes
                                 );
            return unit;
        }
    }
}