using PortfolioApplication.Api.DataTransferObjects;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PortfolioApplication.Api.CQRS.Queries
{
    public interface IQuery<TDto> 
        where TDto : BaseDto
    {
        Task<TDto> Get(int id);
        Task<IEnumerable<TDto>> Get();
    }
}
