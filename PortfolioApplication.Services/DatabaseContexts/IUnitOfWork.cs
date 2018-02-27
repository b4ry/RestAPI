using PortfolioApplication.Entities.Entities;
using System.Threading.Tasks;

namespace PortfolioApplication.Services.DatabaseContexts
{
    public interface IUnitOfWork
    {
        void Save();
        Task SaveAsync();
        void TrackEntity<TEntity>(TEntity entity) where TEntity : BaseEntity;
    }
}
