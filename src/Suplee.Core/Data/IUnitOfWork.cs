using System.Threading.Tasks;

namespace Suplee.Core.Data
{
    public interface IUnitOfWork
    {
        Task<bool> Commit();
    }
}
