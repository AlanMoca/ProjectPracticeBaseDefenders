using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.DataAccess
{
    public interface IUnitsDataAccess
    {
        Task<IReadOnlyList<Unit>> GetAllUnits();
    }
}