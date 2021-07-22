using Domain.Entities;
using System.Threading.Tasks;

namespace Domain.DataAccess
{
    public interface IUserDataAccess
    {
        Task<User> GetLocalUser();
    }
}
    
