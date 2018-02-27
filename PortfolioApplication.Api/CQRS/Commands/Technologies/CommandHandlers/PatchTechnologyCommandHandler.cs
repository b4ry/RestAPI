using AutoMapper;
using Microsoft.Extensions.Caching.Distributed;
using PortfolioApplication.Api.CQRS.Commands.CommandHandlers;
using PortfolioApplication.Api.CQRS.Commands.Technologies.Commands;
using PortfolioApplication.Entities.Entities;
using PortfolioApplication.Services.DatabaseContexts;

namespace PortfolioApplication.Api.CQRS.Commands.Technologies.CommandHandlers
{
    public class PatchTechnologyCommandHandler : PatchEntityCommandHandler<PatchTechnologyCommand, TechnologyEntity>
    {
        public PatchTechnologyCommandHandler(IDatabaseSet databaseSet, IUnitOfWork unitOfWork, IDistributedCache redisCache, IMapper mapper)
            : base(databaseSet, unitOfWork, redisCache, mapper)
        {
        }
    }
}
