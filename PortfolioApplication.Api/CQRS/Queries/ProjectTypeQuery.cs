using AutoMapper;
using Microsoft.Extensions.Caching.Distributed;
using PortfolioApplication.Api.DataTransferObjects.Projects;
using PortfolioApplication.Entities.Entities;
using PortfolioApplication.Services.DatabaseContexts;

namespace PortfolioApplication.Api.CQRS.Queries
{
    public class ProjectTypeQuery : Query<ProjectTypeEntity, ProjectTypeDto>, IProjectTypeQuery
    {
        public ProjectTypeQuery(IDatabaseSet databaseSet, IDistributedCache redisCache, IMapper mapper) : base(databaseSet, redisCache, mapper)
        {
        }
    }
}
