using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using PortfolioApplication.Entities.Entities;
using PortfolioApplication.Services;
using PortfolioApplication.Services.DatabaseContexts;
using System.Threading.Tasks;

namespace PortfolioApplication.Api.CQRS.Commands
{
    public abstract class CreateEntityCommandHandler<TCommand, TEntity> : ICommandHandler<TCommand> 
        where TCommand : ICommand
        where TEntity : BaseEntity
    {
        private IUnitOfWork UnitOfWork { get; }
        private IDatabaseSet DatabaseSet { get; }
        private IDistributedCache RedisCache { get; }
        private DbSet<TEntity> EntitySet { get; }
        private IMapper Mapper { get; }

        public CreateEntityCommandHandler(IDatabaseSet databaseSet, IUnitOfWork unitOfWork, IDistributedCache redisCache, IMapper mapper)
        {
            UnitOfWork = unitOfWork;
            DatabaseSet = databaseSet;
            EntitySet = DatabaseSet.Set<TEntity>();
            RedisCache = redisCache;
            Mapper = mapper;
        }

        public void Handle(TCommand command)
        {
            var entity = Mapper.Map<TEntity>(command);

            EntitySet.Add(entity);
            UnitOfWork.Save();
            RedisCache.Remove(RedisHelper.ComposeRedisKey(typeof(TEntity).Name, "*"));
        }

        public async Task HandleAsync(TCommand command)
        {
            var entity = Mapper.Map<TEntity>(command);

            EntitySet.Add(entity);
            await UnitOfWork.SaveAsync();
            await RedisCache.RemoveAsync(RedisHelper.ComposeRedisKey(typeof(TEntity).Name, "*"));
        }
    }
}
