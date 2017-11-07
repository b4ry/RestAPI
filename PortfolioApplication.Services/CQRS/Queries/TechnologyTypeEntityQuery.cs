using Microsoft.Extensions.Caching.Distributed;
using PortfolioApplication.Entities.Entities;
using PortfolioApplication.Services.DatabaseContext;

namespace PortfolioApplication.Services.CQRS.Queries
{
    public class TechnologyTypeEntityQuery : Query<TechnologyTypeEntity>, ITechnologyTypeEntityQuery
    {
        public TechnologyTypeEntityQuery(IDatabaseSet databaseSet, IDistributedCache redisCache) : base(databaseSet, redisCache)
        {
        }
    }
}
