using DivingApplication.Entities.Entities;
using Microsoft.EntityFrameworkCore;

namespace DivingApplication.Services.DatabaseContext
{
    public interface IDatabaseSet
    {
        DbSet<TEntity> Set<TEntity>() where TEntity : BaseEntity;
    }
}
