using PortfolioApplication.Entities.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PortfolioApplication.Services.CQRS.Queries
{
    public interface IExperienceQuery
    {
        Task<ExperienceEntity> Get(int id);
        Task<IEnumerable<ExperienceEntity>> Get();
    }
}
