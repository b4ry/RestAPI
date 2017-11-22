using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using PortfolioApplication.Entities.Entities;
using PortfolioApplication.Services.DatabaseContext;

namespace PortfolioApplication.Api.CQRS.Commands
{
    public class CreateEntityCommandHandler<TCommand, TEntity> : IHandleCommand<TCommand> 
        where TCommand : ICommand
        where TEntity : BaseEntity
    {
        private IUnitOfWork UnitOfWork { get; }
        private IDatabaseSet DatabaseSet { get; }
        private IDistributedCache RedisCache { get; }
        private DbSet<TEntity> ExperienceSet { get; }

        public CreateEntityCommandHandler(IDatabaseSet databaseSet, IUnitOfWork unitOfWork, IDistributedCache redisCache)
        {
            UnitOfWork = unitOfWork;
            DatabaseSet = databaseSet;
            ExperienceSet = DatabaseSet.Set<TEntity>();
            RedisCache = redisCache;
        }

        public void Handle(TCommand command)
        {
            var entity = Mapper.Map<TEntity>(command);

            ExperienceSet.Add(entity);
            UnitOfWork.Save();
            RedisCache.Remove(ComposeRedisKey(typeof(TEntity).Name, "*"));
        }

        private string ComposeRedisKey(string entityTypeName, string id)
        {
            return entityTypeName + ":" + id;
        }
    }
}
