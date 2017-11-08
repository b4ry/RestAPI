using PortfolioApplication.Entities.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PortfolioApplication.Services.CQRS.Queries
{
    public interface IQuery<TEntity> where TEntity : BaseEntity
    {
        Task<TEntity> Get(int id);
        Task<IEnumerable<TEntity>> Get();
    }
}
