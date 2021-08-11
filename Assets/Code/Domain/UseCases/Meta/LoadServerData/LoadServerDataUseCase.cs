using Code.Domain.DataAccess;
using Code.Domain.Services.Server;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Code.Domain.UseCases.Meta.LoadServerData
{
    public class LoadServerDataUseCase : IServerDataLoader
    {
        private readonly IUnitsDataAccess unitsDataAccess;
        private readonly IServiceAuthenticator serviceAuthenticator;

        public LoadServerDataUseCase( IUnitsDataAccess unitsDataAccess, IServiceAuthenticator serviceAuthenticator )
        {
            this.unitsDataAccess = unitsDataAccess;
            this.serviceAuthenticator = serviceAuthenticator;
        }

        public async Task Load()
        {
            //unitsDataAccess.GetAllUnits();
            //await unitsDataAccess.AddUnitsToUser( serviceAuthenticator.UserId,
            //                                        new List<UnitToAdd> {
            //                                            new UnitToAdd("Unit002",
            //                                            new SharedTypes.Units.UnitState(12, 1))
            //                                        } );
        }
    }
}
//Este no es un interactor porque sólo tiene digamos un "UseCase".