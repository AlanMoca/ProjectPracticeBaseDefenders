using Code.ApplicationLayer.Services.Server.DTOs.User;
using Code.Domain.Services.Serializer;
using System;
using System.Collections.Generic;

namespace Code.ApplicationLayer.Services.Server.Gateways
{
    public class TitleDataGateway : Gateway                                                     //Lo que hace es el mappeador de qué tipo de DTO equivale a cada nombre, ya que en playfab tenemos que preguntar por la key.
    {
        public TitleDataGateway( ISerializerService _serializerService, IGetDataService _getDataService, ISetDataService _setDataService ) : base( _serializerService, _getDataService, _setDataService )
        {
        }

        protected override void InitializeTypeToKey( out Dictionary<Type, string> typeToKey )
        {
            typeToKey = new Dictionary<Type, string>
                        {
                            { typeof(IsInitializedDTO), "IsInitialized" }
                        };
        }
    }
}