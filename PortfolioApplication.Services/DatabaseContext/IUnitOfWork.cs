using System.Threading.Tasks;

namespace PortfolioApplication.Services.DatabaseContext
{
    public interface IUnitOfWork
    {
        void Save();
        Task SaveAsync();
    }
}
