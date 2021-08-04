using Code.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Code.Domain.DataAccess
{
    public interface IUnitsDataAccess
    {
        Task<IReadOnlyList<Unit>> GetAllUnits();
        Task AddUnitsToUser( string userId, List<UnitToAdd> units );
    }
}