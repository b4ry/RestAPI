using Microsoft.EntityFrameworkCore;
using PortfolioApplication.Api.DataTransferObjects.Projects;
using PortfolioApplication.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PortfolioApplication.Api.CQRS.Queries
{
    public interface IProjectQuery
    {
        Task<ProjectDto> GetAsync(int id, Func<DbSet<ProjectEntity>, Task<ProjectEntity>> retrievalFunc);
        Task<IList<ProjectDto>> GetAsync(Func<DbSet<ProjectEntity>, Task<List<ProjectEntity>>> retrievalFunc, string queryParameter = "");
    }
}
