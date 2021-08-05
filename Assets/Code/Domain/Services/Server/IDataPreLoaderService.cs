using System.Threading.Tasks;

namespace Code.Domain.Services.Server
{
    public interface IDataPreLoaderService                                                      //Esta interface la va a implementar el gateway del Inventario, gateway datos del usuario y gateway datos del servidor, por el momento.
    {
        Task PreLoad();
    }
}