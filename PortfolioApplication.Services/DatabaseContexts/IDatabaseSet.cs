using PortfolioApplication.Entities.Entities;
using Microsoft.EntityFrameworkCore;

namespace PortfolioApplication.Services.DatabaseContexts
{
    public interface IDatabaseSet
    {
        DbSet<TEntity> Set<TEntity>() where TEntity : BaseEntity;
    }
}
