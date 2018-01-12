using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using PortfolioApplication.Entities.Entities;
using PortfolioApplication.Services;
using PortfolioApplication.Services.DatabaseContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace PortfolioApplication.Api.CQRS.Commands
{
    public abstract class DeleteEntityCommandHandler<TCommand, TEntity> : ICommandHandler<TCommand, TEntity>
        where TCommand : ICommand
        where TEntity : BaseEntity
    {
        private IUnitOfWork UnitOfWork { get; }
        private IDatabaseSet DatabaseSet { get; }
        private IDistributedCache RedisCache { get; }
        private DbSet<TEntity> EntitySet { get; }
        private IMapper Mapper { get; }

        public DeleteEntityCommandHandler(IDatabaseSet databaseSet, IUnitOfWork unitOfWork, IDistributedCache redisCache, IMapper mapper)
        {
            UnitOfWork = unitOfWork;
            DatabaseSet = databaseSet;
            EntitySet = DatabaseSet.Set<TEntity>();
            RedisCache = redisCache;
            Mapper = mapper;
        }

        public void Handle(TCommand command, Expression<Func<TEntity, bool>> retrievalFunc)
        {
            var entity = Mapper.Map<TEntity>(command);

            try
            {
                entity = EntitySet.Single(predicate: retrievalFunc);
            }
            catch (Exception)
            {
                throw new KeyNotFoundException($"Could not retrieve entity specified by '{command}', type: '{typeof(TEntity).Name}') from database.");
            }

            EntitySet.Remove(entity);
            UnitOfWork.Save();
            RedisCache.Remove(RedisHelper.ComposeRedisKey(typeof(TEntity).Name, entity.Id.ToString()));
            RedisCache.Remove(RedisHelper.ComposeRedisKey(typeof(TEntity).Name, "*"));
        }

        public async Task HandleAsync(TCommand command, Expression<Func<TEntity, bool>> retrievalFunc)
        {
            var entity = Mapper.Map<TEntity>(command);

            try
            {
                entity = await EntitySet.SingleAsync(predicate: retrievalFunc);
            }
            catch (Exception)
            {
                throw new KeyNotFoundException($"Could not retrieve entity specified by '{command}', type: '{typeof(TEntity).Name}') from database.");
            }

            EntitySet.Remove(entity);
            await UnitOfWork.SaveAsync();
            await RedisCache.RemoveAsync(RedisHelper.ComposeRedisKey(typeof(TEntity).Name, entity.Id.ToString()));
            await RedisCache.RemoveAsync(RedisHelper.ComposeRedisKey(typeof(TEntity).Name, "*"));
        }
    }
}
