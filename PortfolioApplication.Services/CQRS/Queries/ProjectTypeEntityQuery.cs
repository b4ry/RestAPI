using Microsoft.Extensions.Caching.Distributed;
using PortfolioApplication.Entities.Entities;
using PortfolioApplication.Services.DatabaseContext;

namespace PortfolioApplication.Services.CQRS.Queries
{
    public class ProjectTypeEntityQuery : Query<ProjectTypeEntity>, IProjectTypeEntityQuery
    {
        public ProjectTypeEntityQuery(IDatabaseSet databaseSet, IDistributedCache redisCache) : base(databaseSet, redisCache)
        {
        }
    }
}
