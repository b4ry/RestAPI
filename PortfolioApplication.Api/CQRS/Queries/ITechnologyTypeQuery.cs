using PortfolioApplication.Api.DataTransferObjects.Technology;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PortfolioApplication.Api.CQRS.Queries
{
    public interface ITechnologyTypeQuery
    {
        Task<TechnologyTypeDto> Get(int id);
        Task<IEnumerable<TechnologyTypeDto>> Get();
    }
}
