using System.Threading.Tasks;

namespace PortfolioApplication.Services.DatabaseContext
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly PortfolioApplicationDbContext _databaseContext;

        public UnitOfWork(PortfolioApplicationDbContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public async Task SaveAsync()
        {
            await _databaseContext.SaveChangesAsync();
        }
    }
}
