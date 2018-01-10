using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using PortfolioApplication.Api.DataTransferObjects;
using PortfolioApplication.Entities.Entities;
using PortfolioApplication.Middlewares.Errors.Exceptions;
using PortfolioApplication.Services;
using PortfolioApplication.Services.DatabaseContexts;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PortfolioApplication.Api.CQRS.Queries
{
    public abstract class Query<TEntity, TDto> : IQuery<TEntity, TDto>
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

        public async Task<TDto> GetAsync(string id, Func<DbSet<TEntity>, Task<TEntity>> retrievalFunc)
        {
            string key = RedisHelper.ComposeRedisKey(typeof(TEntity).Name, id);
            string cachedEntity = await RedisCache.GetStringAsync(key);

            if (string.IsNullOrEmpty(cachedEntity))
            {
                try
                {
                    var retrievedEntity = await DatabaseAccess.RetrieveEntityAsync(retrievalFunc, EntitySet);
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

        public async Task<IList<TDto>> GetAsync(Func<DbSet<TEntity>, Task<List<TEntity>>> retrievalFunc, string queryParameter = "")
        {
            string key = RedisHelper.ComposeRedisKey(typeof(TEntity).Name, $"*{queryParameter}");
            string cachedEntities = await RedisCache.GetStringAsync(key);

            if (string.IsNullOrEmpty(cachedEntities))
            {
                var retrievedEntities = await DatabaseAccess.RetrieveEntitiesAsync(retrievalFunc, EntitySet);

                if (retrievedEntities.Count > 0)
                {
                    var retrievedDtos = Mapper.Map<IEnumerable<TDto>>(retrievedEntities);
                    cachedEntities = JsonConvert.SerializeObject(retrievedDtos);

                    await RedisCache.SetStringAsync(key, cachedEntities);
                }
                else
                {
                    throw new EmptyCollectionException($"Collection of '{typeof(TEntity).Name}' is empty.");
                }
            }

            return JsonConvert.DeserializeObject<IList<TDto>>(cachedEntities);
        }
    }
}
