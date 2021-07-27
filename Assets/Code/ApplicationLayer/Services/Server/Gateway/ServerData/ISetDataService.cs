using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApplicationLayer.Services.Server.Gateways
{
    public interface ISetDataService
    {
        Task Set( Dictionary<string, string> data );
    }
}