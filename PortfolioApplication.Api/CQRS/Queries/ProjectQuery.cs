using AutoMapper;
using Microsoft.Extensions.Caching.Distributed;
using PortfolioApplication.Api.DataTransferObjects.Projects;
using PortfolioApplication.Entities.Entities;
using PortfolioApplication.Services.DatabaseContexts;

namespace PortfolioApplication.Api.CQRS.Queries
{
    public class ProjectQuery : Query<ProjectEntity, ProjectDto>, IProjectQuery
    {
        public ProjectQuery(IDatabaseSet databaseSet, IDistributedCache redisCache, IMapper mapper) : base(databaseSet, redisCache, mapper)
        {
        }
    }
}
