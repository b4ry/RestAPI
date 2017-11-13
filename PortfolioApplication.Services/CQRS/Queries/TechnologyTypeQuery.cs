using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using PortfolioApplication.Entities.Entities;
using PortfolioApplication.Services.DatabaseContext;

namespace PortfolioApplication.Services.CQRS.Queries
{
    public class TechnologyTypeQuery : Query<TechnologyTypeEntity>, ITechnologyTypeQuery
    {
        public TechnologyTypeQuery(IDatabaseSet databaseSet, IDistributedCache redisCache, ILogger<TechnologyTypeEntity> logger) : 
            base(databaseSet, redisCache, logger)
        {
        }
    }
}
