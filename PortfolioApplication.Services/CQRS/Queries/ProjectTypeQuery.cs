using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using PortfolioApplication.Entities.Entities;
using PortfolioApplication.Services.DatabaseContext;

namespace PortfolioApplication.Services.CQRS.Queries
{
    public class ProjectTypeQuery : Query<ProjectTypeEntity>, IProjectTypeQuery
    {
        public ProjectTypeQuery(IDatabaseSet databaseSet, IDistributedCache redisCache, ILogger<ProjectTypeEntity> logger) : 
            base(databaseSet, redisCache, logger)
        {
        }
    }
}
