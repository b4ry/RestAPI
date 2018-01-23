using PortfolioApplication.Entities.Entities;
using PortfolioApplication.Entities.Entities.JunctionEntities;
using PortfolioApplication.Entities.Enums;
using PortfolioApplication.Services.DatabaseContexts;
using System;
using System.Linq;

namespace PortfolioApplication.Api.Extensions
{
    internal static class PortfolioApplicationDbContextExtensions
    {
        internal static void SeedData(this PortfolioApplicationDbContext portfolioApplicationDbContext)
        {
            if (portfolioApplicationDbContext.AllMigrationsApplied())
            {
                SeedTechnologyProjectEnums(portfolioApplicationDbContext);
                SeedTechnologiesProjectsExperiences(portfolioApplicationDbContext);
                SeedProjectTechnologyJunctionTable(portfolioApplicationDbContext);
            }
        }

        private static void SeedProjectTechnologyJunctionTable(PortfolioApplicationDbContext portfolioApplicationDbContext)
        {
            if (!portfolioApplicationDbContext.Set<ProjectTechnologyJunctionEntity>().Any())
            {
                var fullstackProjectId =
                    portfolioApplicationDbContext.Set<ProjectEntity>().Single(x => x.ProjectType.ProjectTypeEnum == ProjectTypeEnum.Fullstack).Id;
                var backendProjectId =
                    portfolioApplicationDbContext.Set<ProjectEntity>().Single(x => x.ProjectType.ProjectTypeEnum == ProjectTypeEnum.Backend).Id;
                var frontendProjectId =
                    portfolioApplicationDbContext.Set<ProjectEntity>().Single(x => x.ProjectType.ProjectTypeEnum == ProjectTypeEnum.Frontend).Id;
                var languageTechnologyId =
                    portfolioApplicationDbContext.Set<TechnologyEntity>().First(x => x.TechnologyType.TechnologyTypeEnum == TechnologyTypeEnum.Language).Id;
                var frameworkTechnologyId =
                    portfolioApplicationDbContext.Set<TechnologyEntity>().First(x => x.TechnologyType.TechnologyTypeEnum == TechnologyTypeEnum.Framework).Id;

                portfolioApplicationDbContext.Set<ProjectTechnologyJunctionEntity>().AddRange(
                    new ProjectTechnologyJunctionEntity() { ProjectId = fullstackProjectId, TechnologyId = languageTechnologyId },
                    new ProjectTechnologyJunctionEntity() { ProjectId = fullstackProjectId, TechnologyId = frameworkTechnologyId },
                    new ProjectTechnologyJunctionEntity() { ProjectId = backendProjectId, TechnologyId = languageTechnologyId },
                    new ProjectTechnologyJunctionEntity() { ProjectId = frontendProjectId, TechnologyId = languageTechnologyId }
                );
            }

            portfolioApplicationDbContext.SaveChanges();
        }

        private static void SeedTechnologiesProjectsExperiences(PortfolioApplicationDbContext portfolioApplicationDbContext)
        {
            if (!portfolioApplicationDbContext.Set<TechnologyEntity>().Any())
            {
                portfolioApplicationDbContext.Set<TechnologyEntity>().AddRange(
                    new TechnologyEntity()
                    {
                        Name = "C#",
                        TechnologyTypeId = portfolioApplicationDbContext.Set<TechnologyTypeEntity>().Single(x => x.TechnologyTypeEnum == TechnologyTypeEnum.Language).Id,
                        IconClass = "devicon-csharp-line-wordmark colored"
                    },

                    new TechnologyEntity()
                    {
                        Name = "Angular",
                        TechnologyTypeId = portfolioApplicationDbContext.Set<TechnologyTypeEntity>().Single(x => x.TechnologyTypeEnum == TechnologyTypeEnum.Framework).Id,
                        IconClass = "devicon-angularjs-plain colored"
                    },

                    new TechnologyEntity()
                    {
                        Name = "CSS",
                        TechnologyTypeId = portfolioApplicationDbContext.Set<TechnologyTypeEntity>().Single(x => x.TechnologyTypeEnum == TechnologyTypeEnum.Language).Id,
                        IconClass = "devicon-css3-plain-wordmark colored"
                    },

                    new TechnologyEntity()
                    {
                        Name = "HTML",
                        TechnologyTypeId = portfolioApplicationDbContext.Set<TechnologyTypeEntity>().Single(x => x.TechnologyTypeEnum == TechnologyTypeEnum.Language).Id,
                        IconClass = "devicon-html5-plain-wordmark colored"
                    },

                    new TechnologyEntity()
                    {
                        Name = "Cucumber",
                        TechnologyTypeId = portfolioApplicationDbContext.Set<TechnologyTypeEntity>().Single(x => x.TechnologyTypeEnum == TechnologyTypeEnum.Framework).Id,
                        IconClass = "devicon-cucumber-plain colored"
                    },

                    new TechnologyEntity()
                    {
                        Name = "Redis",
                        TechnologyTypeId = portfolioApplicationDbContext.Set<TechnologyTypeEntity>().Single(x => x.TechnologyTypeEnum == TechnologyTypeEnum.Tool).Id,
                        IconClass = "devicon-redis-plain colored"
                    }
                );
            }

            if (!portfolioApplicationDbContext.Set<ProjectEntity>().Any())
            {
                portfolioApplicationDbContext.Set<ProjectEntity>().AddRange(
                    new ProjectEntity()
                    {
                        Name = "testProjectFullstack",
                        Description = "testDescriptionFullstack",
                        ProjectTypeId = portfolioApplicationDbContext.Set<ProjectTypeEntity>().Single(x => x.ProjectTypeEnum == ProjectTypeEnum.Fullstack).Id,
                        StartTime = new DateTime(100),
                        EndTime = new DateTime(1000)
                    },

                    new ProjectEntity()
                    {
                        Name = "testProjectBackend",
                        Description = "testDescriptionBackend",
                        ProjectTypeId = portfolioApplicationDbContext.Set<ProjectTypeEntity>().Single(x => x.ProjectTypeEnum == ProjectTypeEnum.Backend).Id,
                        StartTime = new DateTime(2000),
                        EndTime = new DateTime(2500)
                    },

                    new ProjectEntity()
                    {
                        Name = "testProjectFrontend",
                        Description = "testDescriptionFrontend",
                        ProjectTypeId = portfolioApplicationDbContext.Set<ProjectTypeEntity>().Single(x => x.ProjectTypeEnum == ProjectTypeEnum.Frontend).Id,
                        StartTime = new DateTime(3000),
                        EndTime = new DateTime(5000)
                    }
                );
            }

            portfolioApplicationDbContext.SaveChanges();

            if (!portfolioApplicationDbContext.Set<ExperienceEntity>().Any())
            {
                portfolioApplicationDbContext.Set<ExperienceEntity>().AddRange(
                    new ExperienceEntity()
                    {
                        CompanyName = "testCompany",
                        Position = "testJuniorDeveloper",
                        Projects = portfolioApplicationDbContext.Set<ProjectEntity>().ToList()
                    }
                );
            }
        }

        private static void SeedTechnologyProjectEnums(PortfolioApplicationDbContext portfolioApplicationDbContext)
        {
            if (!portfolioApplicationDbContext.Set<TechnologyTypeEntity>().Any())
            {
                portfolioApplicationDbContext.Set<TechnologyTypeEntity>().AddRange(
                    new TechnologyTypeEntity() { Name = "Languages", TechnologyTypeEnum = TechnologyTypeEnum.Language },
                    new TechnologyTypeEntity() { Name = "Frameworks", TechnologyTypeEnum = TechnologyTypeEnum.Framework },
                    new TechnologyTypeEntity() { Name = "Methodologies", TechnologyTypeEnum = TechnologyTypeEnum.Methodology },
                    new TechnologyTypeEntity() { Name = "Tools", TechnologyTypeEnum = TechnologyTypeEnum.Tool }
                );
            }

            if (!portfolioApplicationDbContext.Set<ProjectTypeEntity>().Any())
            {
                portfolioApplicationDbContext.Set<ProjectTypeEntity>().AddRange(
                    new ProjectTypeEntity() { Name = "testBackend", ProjectTypeEnum = ProjectTypeEnum.Backend },
                    new ProjectTypeEntity() { Name = "testFrontend", ProjectTypeEnum = ProjectTypeEnum.Frontend },
                    new ProjectTypeEntity() { Name = "testFullstack", ProjectTypeEnum = ProjectTypeEnum.Fullstack }
                );
            }

            portfolioApplicationDbContext.SaveChanges();
        }
    }
}
