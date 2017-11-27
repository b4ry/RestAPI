using PortfolioApplication.Entities.Entities;
using Microsoft.EntityFrameworkCore;

namespace PortfolioApplication.Services.DatabaseContexts
{
    public class DatabaseSet : IDatabaseSet
    {
        private readonly PortfolioApplicationDbContext _databaseContext;

        public DatabaseSet(PortfolioApplicationDbContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public DbSet<TEntity> Set<TEntity>() where TEntity : BaseEntity
        {
            return _databaseContext.Set<TEntity>();
        }
    }
}
