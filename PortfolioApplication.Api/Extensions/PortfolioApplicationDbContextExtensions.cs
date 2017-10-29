using PortfolioApplication.Entities.Entities;
using PortfolioApplication.Services.DatabaseContext;
using System.Linq;

namespace PortfolioApplication.Api.Extensions
{
    public static class PortfolioApplicationDbContextExtensions
    {
        public static void EnsureSeedData(this PortfolioApplicationDbContext portfolioApplicationDbContext)
        {
            if (portfolioApplicationDbContext.AllMigrationsApplied())
            {
                if (!portfolioApplicationDbContext.Set<TechnologyTypeEntity>().Any())
                {
                }
            }
        }
    }
}
