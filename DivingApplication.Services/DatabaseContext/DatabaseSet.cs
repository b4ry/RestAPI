using DivingApplication.Entities.Entities;
using Microsoft.EntityFrameworkCore;

namespace DivingApplication.Services.DatabaseContext
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
