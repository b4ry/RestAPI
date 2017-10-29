using PortfolioApplication.Entities.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PortfolioApplication.Services.CQRS.Queries
{
    public interface IDivingGearQuery : IQuery
    {
        Task<IList<DivingGear>> GetDivingGearsAsync();
    }
}
