using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using PortfolioApplication.Entities.Entities;
using PortfolioApplication.Services.DatabaseContext;
using PortfolioApplication.Services.Exceptions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PortfolioApplication.Services.CQRS.Queries
{
    public abstract class Query<TEntity> : IQuery<TEntity> where TEntity : BaseEntity
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

        public virtual async Task<TEntity> Get(int id)
        {
            string key = ComposeRedisKey(typeof(TEntity).Name, id.ToString());
            string cachedEntity = await RedisCache.GetStringAsync(key);

            if (string.IsNullOrEmpty(cachedEntity))
            {
                try
                {
                    var retrievedEntity = await EntitySet.SingleAsync(x => x.Id == id);
                    cachedEntity = JsonConvert.SerializeObject(retrievedEntity);

                    await RedisCache.SetStringAsync(key, cachedEntity);
                }
                catch (Exception)
                {
                    throw new KeyNotFoundException($"Could not retrieve entity (id: '{id}', type: '{typeof(TEntity).Name}') from database.");
                }
            }

            return JsonConvert.DeserializeObject<TEntity>(cachedEntity);
        }

        public virtual async Task<IEnumerable<TEntity>> Get()
        {
            string key = ComposeRedisKey(typeof(TEntity).Name, "*");
            string cachedEntities = await RedisCache.GetStringAsync(key);

            if (string.IsNullOrEmpty(cachedEntities))
            {
                if (await EntitySet.CountAsync() > 0)
                {
                    var retrievedEntities = await EntitySet.ToListAsync();
                    cachedEntities = JsonConvert.SerializeObject(retrievedEntities);

                    await RedisCache.SetStringAsync(key, cachedEntities);
                }
                else
                {
                    throw new EmptyCollectionException($"Collection of '{typeof(TEntity).Name}' is empty.");
                }
            }

            return JsonConvert.DeserializeObject<IEnumerable<TEntity>>(cachedEntities);
        }

        protected string ComposeRedisKey(string entityTypeName, string id)
        {
            return entityTypeName + ":" + id;
        }
    }
}
