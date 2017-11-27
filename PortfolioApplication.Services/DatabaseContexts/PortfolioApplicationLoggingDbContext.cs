using Microsoft.EntityFrameworkCore;

namespace PortfolioApplication.Services.DatabaseContexts
{
    public class PortfolioApplicationLoggingDbContext : DbContext
    {
        public PortfolioApplicationLoggingDbContext(DbContextOptions<PortfolioApplicationLoggingDbContext> options) : base(options)
        {

        }
    }
}
