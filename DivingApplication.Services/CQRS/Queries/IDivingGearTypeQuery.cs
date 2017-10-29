using PortfolioApplication.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PortfolioApplication.Services.CQRS.Queries
{
    public interface IDivingGearTypeQuery
    {
        Task<IList<DivingGearType>> GetDivingGearTypesAsync();
    }
}
