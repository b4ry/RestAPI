using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PortfolioApplication.Api.DataTransferObjects.Technologies;
using PortfolioApplication.Entities.Entities;
using PortfolioApplication.Entities.Entities.JunctionEntities;
using PortfolioApplication.Services;
using PortfolioApplication.Services.DatabaseContexts;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PortfolioApplication.Api.Mappings.Resolvers
{
    public class ProjectResolver : IValueResolver<TechnologyDto, TechnologyEntity, IList<ProjectTechnologyJunctionEntity>>//IProjectResolver<TechnologyDto, TechnologyEntity, ProjectTechnologyJunctionEntity>
    {
        protected IDatabaseSet DatabaseSet { get; set; }
        protected DbSet<ProjectEntity> ProjectEntitySet { get; set; }
        protected DbSet<TechnologyEntity> TechnologyEntitySet { get; set; }

        public ProjectResolver(IDatabaseSet databaseSet)
        {
            DatabaseSet = databaseSet;
            ProjectEntitySet = DatabaseSet.Set<ProjectEntity>();
            TechnologyEntitySet = DatabaseSet.Set<TechnologyEntity>();
        }

        public IList<ProjectTechnologyJunctionEntity> Resolve
            (
                TechnologyDto technologyDto, 
                TechnologyEntity technologyEntity, 
                IList<ProjectTechnologyJunctionEntity> destMember, 
                ResolutionContext context
            )
        {
            var projects = new List<ProjectTechnologyJunctionEntity>();

            foreach (var project in technologyDto.Projects)
            {
                Func<DbSet<ProjectEntity>, Task<ProjectEntity>> projectRetrievalFunc =
                    dbSet => dbSet
                    .Include(proj => proj.ProjectType)
                    .Include(proj => proj.Technologies)
                    .ThenInclude(techs => techs.Technology)
                    .ThenInclude(tech => tech.TechnologyType)
                    .SingleAsync(proj => proj.Name == project.Name);

                Func<DbSet<TechnologyEntity>, Task<TechnologyEntity>> technologyRetrievalFunc =
                    dbSet => dbSet.Include(tech => tech.TechnologyType)
                    .Include(tech => tech.Projects)
                    .ThenInclude(projs => projs.Project)
                    .ThenInclude(proj => proj.ProjectType)
                    .SingleAsync(tech => tech.Name == technologyDto.Name);

                var retrievedProject = DatabaseAccess.RetrieveEntityAsync(projectRetrievalFunc, ProjectEntitySet);
                var retrievedTechnology = DatabaseAccess.RetrieveEntityAsync(technologyRetrievalFunc, TechnologyEntitySet);

                var projectTechnologyJunctionEntity = new ProjectTechnologyJunctionEntity();
                projectTechnologyJunctionEntity.Project = retrievedProject.Result;
                projectTechnologyJunctionEntity.Technology = retrievedTechnology.Result;

                projects.Add(projectTechnologyJunctionEntity);
            }

            return projects;
        }
    }
}
