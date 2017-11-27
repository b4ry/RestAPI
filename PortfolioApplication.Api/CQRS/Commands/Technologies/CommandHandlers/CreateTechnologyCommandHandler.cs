using Microsoft.Extensions.Caching.Distributed;
using PortfolioApplication.Api.CQRS.Commands.Technologies.Commands;
using PortfolioApplication.Entities.Entities;
using PortfolioApplication.Services.DatabaseContexts;

namespace PortfolioApplication.Api.CQRS.Commands.Technologies.CommandHandlers
{
    public class CreateTechnologyCommandHandler : CreateEntityCommandHandler<CreateTechnologyCommand, TechnologyEntity>
    {
        public CreateTechnologyCommandHandler(IDatabaseSet databaseSet, IUnitOfWork unitOfWork, IDistributedCache redisCache) : base(databaseSet, unitOfWork, redisCache)
        {
        }
    }
}
