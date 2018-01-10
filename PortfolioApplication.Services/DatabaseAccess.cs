using Microsoft.EntityFrameworkCore;
using PortfolioApplication.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PortfolioApplication.Services
{
    public static class DatabaseAccess
    {
        public static async Task<TEntity> RetrieveEntityAsync<TEntity>(Func<DbSet<TEntity>, Task<TEntity>> retrievalFunc, DbSet<TEntity> entitySet)
            where TEntity : BaseEntity
        {
            return await retrievalFunc(entitySet);
        }

        public static async Task<IList<TEntity>> RetrieveEntitiesAsync<TEntity>(Func<DbSet<TEntity>, Task<List<TEntity>>> retrievalFunc, DbSet<TEntity> entitySet)
            where TEntity : BaseEntity
        {
            return await retrievalFunc(entitySet);
        }
    }
}
