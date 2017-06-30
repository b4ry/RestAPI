using CrankBankAPI.Entity;
using Microsoft.EntityFrameworkCore;

namespace CrankBankAPI.DatabaseContext
{
    public interface IDatabaseSet
    {
        DbSet<TEntity> Set<TEntity>() where TEntity : BaseEntity;
    }
}
