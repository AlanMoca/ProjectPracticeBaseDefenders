using Domain.DataAccess;
using System.Threading.Tasks;

namespace Domain.UseCases.Meta.LoadServerData
{
    public class LoadServerDataUseCase : IServerDataLoader
    {
        private readonly IUnitsDataAccess unitsDataAccess;

        public LoadServerDataUseCase( IUnitsDataAccess _unitsDataAccess )
        {
            this.unitsDataAccess = _unitsDataAccess;
        }

        public async Task Load()
        {
            await unitsDataAccess.GetAllUnits();
        }
    }
}
//Este no es un interactor porque sólo tiene digamos un "UseCase".