using Code.Domain.Entities;
using System.Threading.Tasks;

namespace Code.Domain.DataAccess
{
    public interface IUserDataAccess
    {
        User GetLocalUser();
    }
}
    
