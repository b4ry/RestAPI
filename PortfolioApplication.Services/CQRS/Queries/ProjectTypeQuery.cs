using Microsoft.Extensions.Caching.Distributed;
using PortfolioApplication.Entities.Entities;
using PortfolioApplication.Services.DatabaseContext;

namespace PortfolioApplication.Services.CQRS.Queries
{
    public class ProjectTypeQuery : Query<ProjectTypeEntity>, IProjectTypeQuery
    {
        public ProjectTypeQuery(IDatabaseSet databaseSet, IDistributedCache redisCache) : base(databaseSet, redisCache)
        {
        }
    }
}
