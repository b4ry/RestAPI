using Microsoft.EntityFrameworkCore;

namespace PortfolioApplication.Services.DatabaseContext
{
    public class PortfolioApplicationLoggingDbContext : DbContext
    {
        public PortfolioApplicationLoggingDbContext(DbContextOptions<PortfolioApplicationLoggingDbContext> options) : base(options)
        {

        }
    }
}
