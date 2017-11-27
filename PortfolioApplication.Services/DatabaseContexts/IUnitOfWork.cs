using System.Threading.Tasks;

namespace PortfolioApplication.Services.DatabaseContexts
{
    public interface IUnitOfWork
    {
        void Save();
        Task SaveAsync();
    }
}
