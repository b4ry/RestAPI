using Microsoft.Extensions.Caching.Distributed;
using PortfolioApplication.Api.DataTransferObjects.Project;
using PortfolioApplication.Entities.Entities;
using PortfolioApplication.Services.DatabaseContext;

namespace PortfolioApplication.Api.CQRS.Queries
{
    public class ProjectTypeQuery : Query<ProjectTypeEntity, ProjectTypeDto>, IProjectTypeQuery
    {
        public ProjectTypeQuery(IDatabaseSet databaseSet, IDistributedCache redisCache) : 
            base(databaseSet, redisCache)
        {
        }
    }
}
