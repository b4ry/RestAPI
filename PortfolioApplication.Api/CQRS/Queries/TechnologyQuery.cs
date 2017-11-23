using Microsoft.Extensions.Caching.Distributed;
using PortfolioApplication.Api.DataTransferObjects.Technologies;
using PortfolioApplication.Entities.Entities;
using PortfolioApplication.Services.DatabaseContext;

namespace PortfolioApplication.Api.CQRS.Queries
{
    public class TechnologyQuery : Query<TechnologyEntity, TechnologyDto>, ITechnologyQuery
    {
        public TechnologyQuery(IDatabaseSet databaseSet, IDistributedCache redisCache) :
            base(databaseSet, redisCache)
        {
        }
    }
}
