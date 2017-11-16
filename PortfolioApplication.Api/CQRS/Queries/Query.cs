using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using PortfolioApplication.Api.DataTransferObjects;
using PortfolioApplication.Entities.Entities;
using PortfolioApplication.Middlewares.Errors.Exceptions;
using PortfolioApplication.Services.DatabaseContext;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PortfolioApplication.Api.CQRS.Queries
{
    public abstract class Query<TEntity, TDto> : IQuery<TDto> 
        where TEntity : BaseEntity
        where TDto : BaseDto
    {
        protected IDatabaseSet DatabaseSet { get; }
        protected DbSet<TEntity> EntitySet { get; }
        protected IDistributedCache RedisCache { get; }

        public Query(IDatabaseSet databaseSet, IDistributedCache redisCache)
        {
            DatabaseSet = databaseSet;
            EntitySet = DatabaseSet.Set<TEntity>();
            RedisCache = redisCache;
        }

        public virtual async Task<TDto> Get(int id)
        {
            string key = ComposeRedisKey(typeof(TEntity).Name, id.ToString());
            string cachedEntity = await RedisCache.GetStringAsync(key);

            if (string.IsNullOrEmpty(cachedEntity))
            {
                try
                {
                    var retrievedEntity = await EntitySet.SingleAsync(x => x.Id == id);
                    var retrievedDto = Mapper.Map<TDto>(retrievedEntity);
                    cachedEntity = JsonConvert.SerializeObject(retrievedDto);

                    await RedisCache.SetStringAsync(key, cachedEntity);
                }
                catch (Exception)
                {
                    throw new KeyNotFoundException($"Could not retrieve entity (id: '{id}', type: '{typeof(TEntity).Name}') from database.");
                }
            }

            return JsonConvert.DeserializeObject<TDto>(cachedEntity);
        }

        public virtual async Task<IEnumerable<TDto>> Get()
        {
            string key = ComposeRedisKey(typeof(TEntity).Name, "*");
            string cachedEntities = await RedisCache.GetStringAsync(key);

            if (string.IsNullOrEmpty(cachedEntities))
            {
                if (await EntitySet.CountAsync() > 0)
                {
                    var retrievedEntities = await EntitySet.ToListAsync();
                    var retrievedDtos = Mapper.Map <IEnumerable<TDto>>(retrievedEntities);
                    cachedEntities = JsonConvert.SerializeObject(retrievedDtos);

                    await RedisCache.SetStringAsync(key, cachedEntities);
                }
                else
                {
                    throw new EmptyCollectionException($"Collection of '{typeof(TEntity).Name}' is empty.");
                }
            }

            return JsonConvert.DeserializeObject<IEnumerable<TDto>>(cachedEntities);
        }

        protected string ComposeRedisKey(string entityTypeName, string id)
        {
            return entityTypeName + ":" + id;
        }
    }
}
