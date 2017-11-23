using Microsoft.Extensions.Caching.Distributed;
using PortfolioApplication.Api.DataTransferObjects.Projects;
using PortfolioApplication.Entities.Entities;
using PortfolioApplication.Services.DatabaseContext;

namespace PortfolioApplication.Api.CQRS.Queries
{
    public class ProjectQuery : Query<ProjectEntity, ProjectDto>, IProjectQuery
    {
        public ProjectQuery(IDatabaseSet databaseSet, IDistributedCache redisCache) : base(databaseSet, redisCache)
        {
        }
    }
}
