using PortfolioApplication.Api.DataTransferObjects.Project;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PortfolioApplication.Api.CQRS.Queries
{
    public interface IProjectTypeQuery
    {
        Task<ProjectTypeDto> Get(int id);
        Task<IEnumerable<ProjectTypeDto>> Get();
    }
}
