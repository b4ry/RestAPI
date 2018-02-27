using Microsoft.EntityFrameworkCore;
using PortfolioApplication.Entities.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace PortfolioApplication.Services.DatabaseContexts
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly PortfolioApplicationDbContext _databaseContext;

        public UnitOfWork(PortfolioApplicationDbContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public void Save()
        {
            _databaseContext.SaveChanges();
        }

        public async Task SaveAsync()
        {
            await _databaseContext.SaveChangesAsync();
        }

        public void TrackEntity<TEntity>(TEntity entity) where TEntity : BaseEntity
        {
            _databaseContext.Set<TEntity>().Attach(entity);
            _databaseContext.ChangeTracker.Entries<TEntity>().Single(e => e.Entity.Equals(entity)).State = EntityState.Modified;
        }
    }
}
