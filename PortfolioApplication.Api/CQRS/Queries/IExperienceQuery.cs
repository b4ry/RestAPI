using Microsoft.EntityFrameworkCore;
using PortfolioApplication.Api.DataTransferObjects;
using PortfolioApplication.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PortfolioApplication.Api.CQRS.Queries
{
    public interface IExperienceQuery
    {
        Task<ExperienceDto> GetAsync(string id, Func<DbSet<ExperienceEntity>, Task<ExperienceEntity>> retrievalFunc);
        Task<IList<ExperienceDto>> GetAsync(Func<DbSet<ExperienceEntity>, Task<List<ExperienceEntity>>> retrievalFunc, string queryParameter = "");
    }
}
