using AutoMapper;
using Microsoft.Extensions.Caching.Distributed;
using PortfolioApplication.Api.DataTransferObjects.Technologies;
using PortfolioApplication.Entities.Entities;
using PortfolioApplication.Services.DatabaseContexts;

namespace PortfolioApplication.Api.CQRS.Queries
{
    public class TechnologyQuery : Query<TechnologyEntity, TechnologyDto>, ITechnologyQuery
    {
        public TechnologyQuery(IDatabaseSet databaseSet, IDistributedCache redisCache, IMapper mapper) :
            base(databaseSet, redisCache, mapper)
        {
        }
    }
}
