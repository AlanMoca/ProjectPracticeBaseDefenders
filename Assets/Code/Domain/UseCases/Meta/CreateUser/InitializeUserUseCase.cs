using Code.Domain.DataAccess;
using Code.SharedTypes.Units;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Code.Domain.UseCases.Meta.CreateUser
{
    public class InitializeUserUseCase : IUserInitializer
    {
        private readonly IUserDataAccess userDataAcess;
        private readonly IUnitsDataAccess unitsDataAccess;

        public InitializeUserUseCase( IUserDataAccess userDataAcess, IUnitsDataAccess unitsDataAccess )
        {
            this.userDataAcess = userDataAcess;
            this.unitsDataAccess = unitsDataAccess;
        }

        public async void Init()
        {
            if( userDataAcess.IsNewUser() )
            {
                var unitsId = unitsDataAccess.GetInitialUnitsId();
                var unitToAdds = new List<UnitToAdd>( unitsId.Select( unitId =>
                                                                    {
                                                                        var unitToAddd = new UnitToAdd( unitId,
                                                                                                        UnitToAdd.InitialUnitsAnnotation,
                                                                                                        new UnitState( 1 ) );
                                                                        return unitToAddd;
                                                                    } ) );

                var userId = userDataAcess.GetUserId();
                await unitsDataAccess.AddUnitsToUser( userId, unitToAdds );

                await userDataAcess.CreateLocalUser();
            }
        }

    }
}