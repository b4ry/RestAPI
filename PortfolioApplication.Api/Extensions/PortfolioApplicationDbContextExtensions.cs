using PortfolioApplication.Entities.Entities;
using PortfolioApplication.Entities.Enums;
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
                    portfolioApplicationDbContext.Set<TechnologyTypeEntity>().AddRange(
                        new TechnologyTypeEntity("testFramework", TechnologyTypeEnum.Framework),
                        new TechnologyTypeEntity("testLanguage", TechnologyTypeEnum.Language),
                        new TechnologyTypeEntity("testMethodology", TechnologyTypeEnum.Methodology),
                        new TechnologyTypeEntity("testTool", TechnologyTypeEnum.Tool)
                        );
                }
            }

            portfolioApplicationDbContext.SaveChanges();
        }
    }
}
