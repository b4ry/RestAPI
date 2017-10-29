using PortfolioApplication.Entities.Entities;
using Microsoft.EntityFrameworkCore;

namespace PortfolioApplication.Services.DatabaseContext
{
    public interface IDatabaseSet
    {
        DbSet<TEntity> Set<TEntity>() where TEntity : BaseEntity;
    }
}
