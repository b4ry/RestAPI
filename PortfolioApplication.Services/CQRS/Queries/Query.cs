using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using PortfolioApplication.Entities.Entities;
using PortfolioApplication.Services.DatabaseContext;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using PortfolioApplication.Services.Exceptions;

namespace PortfolioApplication.Services.CQRS.Queries
{
    public abstract class Query<TEntity> : IQuery<TEntity> where TEntity : BaseEntity
    {
        protected IDatabaseSet DatabaseSet { get; }
        protected DbSet<TEntity> EntitySet { get; }
        protected IDistributedCache RedisCache { get; }
        protected ILogger<TEntity> Logger { get; }

        public Query(IDatabaseSet databaseSet, IDistributedCache redisCache, ILogger<TEntity> logger)
        {
            DatabaseSet = databaseSet;
            EntitySet = DatabaseSet.Set<TEntity>();
            RedisCache = redisCache;
            Logger = logger;
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
                    Logger.LogError(e, $"Could not retrieve entity (id: '{id}') from database.");

                    throw new KeyNotFoundException($"Could not retrieve entity (id: '{id}') from database.");
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
                if (await EntitySet.CountAsync() > 0)
                {
                    var retrievedEntities = await EntitySet.ToListAsync();
                    cachedEntities = JsonConvert.SerializeObject(retrievedEntities);

                    await RedisCache.SetStringAsync(key, cachedEntities);
                }
                else
                {
                    Logger.LogInformation("Collection is empty.");

                    throw new EmptyCollectionException("Collection is empty.");
                }
            }

            return JsonConvert.DeserializeObject<IEnumerable<TEntity>>(cachedEntities);
        }

        private string ComposeRedisKey(string entityTypeName, string id)
        {
            return entityTypeName + ":" + id;
        }
    }
}
