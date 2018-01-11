using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using PortfolioApplication.Entities.Entities;
using PortfolioApplication.Services;
using PortfolioApplication.Services.DatabaseContexts;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;

namespace PortfolioApplication.Api.CQRS.Commands.CommandHandlers
{
    public abstract class PatchEntityCommandHandler<TCommand, TEntity> : ICommandHandler<TCommand, TEntity>
        where TCommand : ICommand
        where TEntity : BaseEntity
    {
        private IUnitOfWork _unitOfWork;
        private IDatabaseSet _databaseSet;
        private IDistributedCache _redisCache;
        private DbSet<TEntity> _entitySet;

        public PatchEntityCommandHandler(IDatabaseSet databaseSet, IUnitOfWork unitOfWork, IDistributedCache redisCache)
        {
            _unitOfWork = unitOfWork;
            _databaseSet = databaseSet;
            _entitySet = _databaseSet.Set<TEntity>();
            _redisCache = redisCache;
        }

        public void Handle(TCommand command, Expression<Func<TEntity, bool>> retrievalFunc)
        {
            throw new NotImplementedException();
        }

        public async Task HandleAsync(TCommand command, Expression<Func<TEntity, bool>> retrievalFunc)
        {
            TEntity entity = null;

            try
            {
                entity = await _entitySet.SingleAsync(predicate: retrievalFunc);
            }
            catch (Exception)
            {
                throw new KeyNotFoundException($"Could not retrieve entity specified by '{command}', type: '{typeof(TEntity).Name}') from database.");
            }

            var properties = command.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);

            foreach (var property in properties)
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

            await _unitOfWork.SaveAsync();

            string redisKey = RedisHelper.ComposeRedisKey(typeof(TEntity).Name, entity.Id.ToString());
            string serializedEntity = JsonConvert.SerializeObject(entity);

            await _redisCache.RemoveAsync(redisKey);
            await _redisCache.RemoveAsync(RedisHelper.ComposeRedisKey(typeof(TEntity).Name, "*"));
            await _redisCache.SetStringAsync(redisKey, serializedEntity);
        }
    }
}
