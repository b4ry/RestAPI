using System.Threading.Tasks;

namespace PortfolioApplication.Services.DatabaseContext
{
    public interface IUnitOfWork
    {
        Task SaveAsync();
    }
}
