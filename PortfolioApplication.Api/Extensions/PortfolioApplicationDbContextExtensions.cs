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
                        new TechnologyTypeEntity() { Name = "testFramework", TechnologyTypeEnum = TechnologyTypeEnum.Framework },
                        new TechnologyTypeEntity() { Name = "testLanguage", TechnologyTypeEnum = TechnologyTypeEnum.Language },
                        new TechnologyTypeEntity() { Name = "testMethodology", TechnologyTypeEnum = TechnologyTypeEnum.Methodology },
                        new TechnologyTypeEntity() { Name = "testTool", TechnologyTypeEnum = TechnologyTypeEnum.Tool }
                        );
                }
            }

            portfolioApplicationDbContext.SaveChanges();
        }
    }
}
