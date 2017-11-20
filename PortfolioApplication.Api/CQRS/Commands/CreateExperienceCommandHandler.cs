using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using PortfolioApplication.Entities.Entities;
using PortfolioApplication.Services.DatabaseContext;

namespace PortfolioApplication.Api.CQRS.Commands
{
    public class CreateExperienceCommandHandler : IHandleCommand<CreateExperienceCommand>
    {
        private IUnitOfWork UnitOfWork { get; }
        private IDatabaseSet DatabaseSet { get; }
        private DbSet<ExperienceEntity> ExperienceSet { get; }
        private IDistributedCache RedisCache { get; }

        public CreateExperienceCommandHandler(IDatabaseSet databaseSet, IUnitOfWork unitOfWork, IDistributedCache redisCache)
        {
            UnitOfWork = unitOfWork;
            DatabaseSet = databaseSet;
            ExperienceSet = DatabaseSet.Set<ExperienceEntity>();
            RedisCache = redisCache;
        }

        public void Handle(CreateExperienceCommand command)
        {
            var experienceEntity = Mapper.Map<ExperienceEntity>(command);

            ExperienceSet.Add(experienceEntity);

            UnitOfWork.Save();
            RedisCache.Remove(ComposeRedisKey(typeof(ExperienceEntity).Name, "*"));
        }

        private string ComposeRedisKey(string entityTypeName, string id)
        {
            return entityTypeName + ":" + id;
        }
    }
}
