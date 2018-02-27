using Microsoft.EntityFrameworkCore;
using PortfolioApplication.Api.DataTransferObjects.Technologies;
using PortfolioApplication.Entities.Entities;
using System;
using System.Threading.Tasks;

namespace PortfolioApplication.Api.CQRS.Queries
{
    public interface IPatchTechnologyQuery
    {
        Task<PatchTechnologyDto> GetAsync(string id, Func<DbSet<TechnologyEntity>, Task<TechnologyEntity>> retrievalFunc);
    }
}
