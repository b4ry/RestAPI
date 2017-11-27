using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using PortfolioApplication.Entities.Entities;
using PortfolioApplication.Services;
using PortfolioApplication.Services.DatabaseContext;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;

namespace PortfolioApplication.Api.CQRS.Commands
{
    public class UpdateEntityCommandHandler<TCommand, TEntity> : IHandleCommand<TCommand, TEntity>
        where TCommand : ICommand
        where TEntity : BaseEntity
    {
        private IUnitOfWork UnitOfWork { get; }
        private IDatabaseSet DatabaseSet { get; }
        private IDistributedCache RedisCache { get; }
        private DbSet<TEntity> EntitySet { get; }

        public UpdateEntityCommandHandler(IDatabaseSet databaseSet, IUnitOfWork unitOfWork, IDistributedCache redisCache)
        {
            UnitOfWork = unitOfWork;
            DatabaseSet = databaseSet;
            EntitySet = DatabaseSet.Set<TEntity>();
            RedisCache = redisCache;
        }

        public void Handle(TCommand command, Expression<Func<TEntity, bool>> retrievalFunc)
        {
            var entity = Mapper.Map<TEntity>(command);
            entity = EntitySet.Single(predicate: retrievalFunc);

            EntitySet.Remove(entity);
            UnitOfWork.Save();
            RedisCache.Remove(RedisHelper.ComposeRedisKey(typeof(TEntity).Name, entity.Id.ToString()));
            RedisCache.Remove(RedisHelper.ComposeRedisKey(typeof(TEntity).Name, "*"));
        }

        public async Task HandleAsync(TCommand command, Expression<Func<TEntity, bool>> retrievalFunc)
        {
            var entity = Mapper.Map<TEntity>(command);
            entity = await EntitySet.SingleAsync(predicate: retrievalFunc);

            var properties = command.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);
            
            foreach(var property in properties)
            {
                var propertyToUpdate = entity.GetType().GetProperty(property.Name);

                if (propertyToUpdate != null)
                {
                    var value = property.GetValue(command);

                    if (value != null)
                    {
                        propertyToUpdate.SetValue(entity, value);
                    }
                }
            }

            await UnitOfWork.SaveAsync();

            string redisKey = RedisHelper.ComposeRedisKey(typeof(TEntity).Name, entity.Id.ToString());
            string serializedEntity = JsonConvert.SerializeObject(entity);

            await RedisCache.RemoveAsync(redisKey);
            await RedisCache.RemoveAsync(RedisHelper.ComposeRedisKey(typeof(TEntity).Name, "*"));
            await RedisCache.SetStringAsync(redisKey, serializedEntity);
        }
    }
}
