using AutoMapper;
using Microsoft.Extensions.Caching.Distributed;
using PortfolioApplication.Api.DataTransferObjects.Technologies;
using PortfolioApplication.Entities.Entities;
using PortfolioApplication.Services.DatabaseContexts;

namespace PortfolioApplication.Api.CQRS.Queries
{
    public class PatchTechnologyQuery : Query<TechnologyEntity, PatchTechnologyDto>, IPatchTechnologyQuery
    {
        public PatchTechnologyQuery(IDatabaseSet databaseSet, IDistributedCache redisCache, IMapper mapper) :
            base(databaseSet, redisCache, mapper)
        {
        }
    }
}
