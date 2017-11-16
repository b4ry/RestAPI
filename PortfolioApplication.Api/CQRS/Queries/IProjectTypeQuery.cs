using PortfolioApplication.Entities.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PortfolioApplication.Api.CQRS.Queries
{
    public interface IProjectTypeQuery
    {
        Task<ProjectTypeEntity> Get(int id);
        Task<IEnumerable<ProjectTypeEntity>> Get();
    }
}
