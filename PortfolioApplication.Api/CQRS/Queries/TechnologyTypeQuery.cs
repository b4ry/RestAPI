using Microsoft.Extensions.Caching.Distributed;
using PortfolioApplication.Entities.Entities;
using PortfolioApplication.Services.DatabaseContext;

namespace PortfolioApplication.Api.CQRS.Queries
{
    public class TechnologyTypeQuery : Query<TechnologyTypeEntity>, ITechnologyTypeQuery
    {
        public TechnologyTypeQuery(IDatabaseSet databaseSet, IDistributedCache redisCache) : 
            base(databaseSet, redisCache)
        {
        }
    }
}
