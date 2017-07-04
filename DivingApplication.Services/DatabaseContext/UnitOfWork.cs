using System.Threading.Tasks;

namespace DivingApplication.Services.DatabaseContext
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DivingApplicationDbContext _databaseContext;

        public UnitOfWork(DivingApplicationDbContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public async Task SaveAsync()
        {
            await _databaseContext.SaveChangesAsync();
        }
    }
}
