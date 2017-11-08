using PortfolioApplication.Entities.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PortfolioApplication.Services.CQRS.Queries
{
    public interface IProjectTypeEntityQuery
    {
        Task<ProjectTypeEntity> Get(int id);
        Task<IEnumerable<ProjectTypeEntity>> Get();
    }
}
