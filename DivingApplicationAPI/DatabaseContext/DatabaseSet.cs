using DivingApplicationAPI.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DivingApplicationAPI.DatabaseContext
{
    public class DatabaseSet : IDatabaseSet
    {
        private readonly DivingApplicationDbContext _databaseContext;

        public DatabaseSet(DivingApplicationDbContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public DbSet<TEntity> Set<TEntity>() where TEntity : BaseEntity
        {
            return _databaseContext.Set<TEntity>();
        }
    }
}
