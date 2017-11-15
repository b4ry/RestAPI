using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;
using PortfolioApplication.Entities.Entities;
using PortfolioApplication.Services.DatabaseContext;
using Newtonsoft.Json;
using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace PortfolioApplication.Services.CQRS.Queries
{
    public class ExperienceQuery : Query<ExperienceEntity>, IExperienceQuery
    {
        public ExperienceQuery(IDatabaseSet databaseSet, IDistributedCache redisCache) : 
            base(databaseSet, redisCache)
        {
        }

        public async override Task<ExperienceEntity> Get(int id)
        {
            string key = ComposeRedisKey(typeof(ExperienceEntity).Name, id.ToString());
            string cachedEntity = await RedisCache.GetStringAsync(key);

            if (string.IsNullOrEmpty(cachedEntity))
            {
                try
                {
                    var retrievedEntity = await EntitySet
                        .Include(exp => exp.Projects)
                        .ThenInclude(proj => proj.Technologies)
                        .ThenInclude(tech => tech.Technology)
                        .ThenInclude(tech1 => tech1.TechnologyType)
                        .Include(exp => exp.Projects)
                        .ThenInclude(proj => proj.ProjectType)
                        .SingleAsync(exp => exp.Id == id);

                    cachedEntity = JsonConvert.SerializeObject(retrievedEntity, new JsonSerializerSettings
                    {
                        ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                    });

                    await RedisCache.SetStringAsync(key, cachedEntity);
                }
                catch (Exception)
                {
                    throw new KeyNotFoundException($"Could not retrieve entity (id: '{id}', type: '{typeof(ExperienceEntity).Name}') from database.");
                }
            }

            return JsonConvert.DeserializeObject<ExperienceEntity>(cachedEntity);
        }
    }
}
