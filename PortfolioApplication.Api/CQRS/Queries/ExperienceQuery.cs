using Microsoft.Extensions.Caching.Distributed;
using PortfolioApplication.Api.DataTransferObjects;
using PortfolioApplication.Entities.Entities;
using PortfolioApplication.Services.DatabaseContexts;

namespace PortfolioApplication.Api.CQRS.Queries
{
    public class ExperienceQuery : Query<ExperienceEntity, ExperienceDto>, IExperienceQuery
    {
        public ExperienceQuery(IDatabaseSet databaseSet, IDistributedCache redisCache) : base(databaseSet, redisCache)
        {
        }
    }
}
