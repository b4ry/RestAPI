using Microsoft.EntityFrameworkCore;
using PortfolioApplication.Api.DataTransferObjects.Technologies;
using PortfolioApplication.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PortfolioApplication.Api.CQRS.Queries
{
    public interface ITechnologyQuery
    {
        Task<TechnologyDto> Get(int id, Func<DbSet<TechnologyEntity>, Task<TechnologyEntity>> retrievalFunc);
        Task<IList<TechnologyDto>> Get(Func<DbSet<TechnologyEntity>, Task<List<TechnologyEntity>>> retrievalFunc);
    }
}
