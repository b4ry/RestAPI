using PortfolioApplication.Api.DataTransferObjects;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PortfolioApplication.Api.CQRS.Queries
{
    public interface IExperienceQuery
    {
        Task<ExperienceDto> Get(int id);
        Task<IEnumerable<ExperienceDto>> Get();
    }
}
