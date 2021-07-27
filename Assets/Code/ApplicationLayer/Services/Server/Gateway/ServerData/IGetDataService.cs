using System.Collections.Generic;
using System.Threading.Tasks;
using SystemUtilities;

namespace ApplicationLayer.Services.Server.Gateways
{
    public interface IGetDataService
    {
        Task<Optional<DataResult>> Get( List<string> keys );
    }
}
