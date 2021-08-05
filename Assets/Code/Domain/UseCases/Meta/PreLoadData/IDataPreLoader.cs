using System.Threading.Tasks;

namespace Code.Domain.UseCases.Meta.PreLoadData
{
    public interface IDataPreLoader
    {
        Task PreLoad();
    }
}