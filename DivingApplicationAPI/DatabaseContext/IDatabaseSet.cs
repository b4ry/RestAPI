using DivingApplicationAPI.Entity;
using Microsoft.EntityFrameworkCore;

namespace DivingApplicationAPI.DatabaseContext
{
    public interface IDatabaseSet
    {
        DbSet<TEntity> Set<TEntity>() where TEntity : BaseEntity;
    }
}
