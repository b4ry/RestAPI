using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using PortfolioApplication.Entities.Entities;
using PortfolioApplication.Services.DatabaseContext;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;

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

        public async Task<TEntity> Get(int id)
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
                catch (Exception e)
                {
                    throw new NotImplementedException(e.Message);
                }
            }

            return JsonConvert.DeserializeObject<TEntity>(cachedEntity);
        }

        public async Task<IEnumerable<TEntity>> Get()
        {
            string key = ComposeRedisKey(typeof(TEntity).Name, "*");
            string cachedEntities = await RedisCache.GetStringAsync(key);

            if (string.IsNullOrEmpty(cachedEntities))
            {
                var retrievedEntities = await EntitySet.ToListAsync();
                cachedEntities = JsonConvert.SerializeObject(retrievedEntities);

                await RedisCache.SetStringAsync(key, cachedEntities);
            }

            return JsonConvert.DeserializeObject<IEnumerable<TEntity>>(cachedEntities);
        }

        private string ComposeRedisKey(string entityTypeName, string id)
        {
            return entityTypeName + ":" + id;
        }
    }
}
