using Microsoft.Extensions.Caching.Distributed;
using PortfolioApplication.Entities.Entities;
using PortfolioApplication.Services.DatabaseContext;

namespace PortfolioApplication.Api.CQRS.Commands.Experiences
{
    public class DeleteExperienceCommandHandler : DeleteEntityCommandHandler<DeleteExperienceCommand, ExperienceEntity>
    {
        public DeleteExperienceCommandHandler(IDatabaseSet databaseSet, IUnitOfWork unitOfWork, IDistributedCache redisCache) : base(databaseSet, unitOfWork, redisCache)
        {
        }
    }
}
