using ApplicationLayer.Services.Server.DTOs.User;
using Domain.Services.Serializer;
using System;
using System.Collections.Generic;

namespace ApplicationLayer.Services.Server.Gateways
{
    public class UserDataGateway : Gateway                                                      //Lo que hace es el mappeador de qué tipo de DTO equivale a cada nombre, ya que en playfab tenemos que preguntar por la key.
    {
        public UserDataGateway( ISerializerService _serializerService, IGetDataService _getDataService, ISetDataService _setDataService ) : base( _serializerService, _getDataService, _setDataService )
        {
        }

        protected override void InitializeTypeToKey( out Dictionary<Type, string> typeToKey )
        {
            typeToKey = new Dictionary<Type, string>
                        {
                            { typeof(IsInitializedDTO), "IsInitialized" }                       //Mapeo de para este tipo utiliza esta key para cuando nos comuniquemos con playfab.
                        };
        }
    }
}

