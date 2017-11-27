using Microsoft.Extensions.Caching.Distributed;
using PortfolioApplication.Api.DataTransferObjects.Technologies;
using PortfolioApplication.Entities.Entities;
using PortfolioApplication.Services.DatabaseContexts;

namespace PortfolioApplication.Api.CQRS.Queries
{
    public class TechnologyTypeQuery : Query<TechnologyTypeEntity, TechnologyTypeDto>, ITechnologyTypeQuery
    {
        public TechnologyTypeQuery(IDatabaseSet databaseSet, IDistributedCache redisCache) : 
            base(databaseSet, redisCache)
        {
        }
    }
}
