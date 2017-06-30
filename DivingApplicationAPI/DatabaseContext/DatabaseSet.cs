using CrankBankAPI.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrankBankAPI.DatabaseContext
{
    public class DatabaseSet : IDatabaseSet
    {
        private readonly CrankBankDbContext _databaseContext;

        public DatabaseSet(CrankBankDbContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public DbSet<TEntity> Set<TEntity>() where TEntity : BaseEntity
        {
            return _databaseContext.Set<TEntity>();
        }
    }
}
