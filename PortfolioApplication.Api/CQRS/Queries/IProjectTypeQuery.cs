using Microsoft.EntityFrameworkCore;
using PortfolioApplication.Api.DataTransferObjects.Projects;
using PortfolioApplication.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PortfolioApplication.Api.CQRS.Queries
{
    public interface IProjectTypeQuery
    {
        Task<ProjectTypeDto> Get(int id, Func<DbSet<ProjectTypeEntity>, Task<ProjectTypeEntity>> retrievalFunc);
        Task<IList<ProjectTypeDto>> Get(Func<DbSet<ProjectTypeEntity>, Task<List<ProjectTypeEntity>>> retrievalFunc);
    }
}
