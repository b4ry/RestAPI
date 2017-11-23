using Microsoft.EntityFrameworkCore;
using PortfolioApplication.Api.DataTransferObjects.Technologies;
using PortfolioApplication.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PortfolioApplication.Api.CQRS.Queries
{
    public interface ITechnologyTypeQuery
    {
        Task<TechnologyTypeDto> GetAsync(int id, Func<DbSet<TechnologyTypeEntity>, Task<TechnologyTypeEntity>> retrievalFunc);
        Task<IList<TechnologyTypeDto>> GetAsync(Func<DbSet<TechnologyTypeEntity>, Task<List<TechnologyTypeEntity>>> retrievalFunc);
    }
}
