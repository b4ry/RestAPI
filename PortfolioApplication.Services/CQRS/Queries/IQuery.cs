using PortfolioApplication.Entities.Entities;
using System.Threading.Tasks;

namespace PortfolioApplication.Services.CQRS.Queries
{
    public interface IQuery<TEntity> where TEntity : BaseEntity
    {
        Task<TEntity> Get(int id);
    }
}
