using PortfolioApplication.Entities.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PortfolioApplication.Services.CQRS.Queries
{
    public interface ITechnologyTypeQuery
    {
        Task<TechnologyTypeEntity> Get(int id);
        Task<IEnumerable<TechnologyTypeEntity>> Get();
    }
}
