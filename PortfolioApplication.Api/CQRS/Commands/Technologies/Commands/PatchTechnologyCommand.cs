using AutoMapper;
using PortfolioApplication.Api.DataTransferObjects;
using PortfolioApplication.Api.DataTransferObjects.Projects;
using PortfolioApplication.Api.DataTransferObjects.Technologies;
using PortfolioApplication.Entities.Entities;
using PortfolioApplication.Entities.Entities.JunctionEntities;
using PortfolioApplication.Entities.Enums;
using System.Collections.Generic;

namespace PortfolioApplication.Api.CQRS.Commands.Technologies.Commands
{
    public class PatchTechnologyCommand : IPatchTechnologyCommand
    {
        public string Name { get; }
        public IList<ProjectTechnologyJunctionEntity> Projects { get; }
        public TechnologyTypeEnum TechnologyTypeEnum { get; }

        private IMapper _mapper;

        public PatchTechnologyCommand(string name, IList<TechnologyProjectDto> projects, TechnologyTypeEnum technologyTypeEnum, IMapper mapper)
        {
            _mapper = mapper;

            Name = name;
            TechnologyTypeEnum = technologyTypeEnum;
            Projects = new List<ProjectTechnologyJunctionEntity>();

            foreach(var project in projects)
            {
                var projectTechnologyJunctionEntity = new ProjectTechnologyJunctionEntity();

                projectTechnologyJunctionEntity.Project = new ProjectEntity();
                projectTechnologyJunctionEntity.Project.Name = project.Name;

                projectTechnologyJunctionEntity.Technology = new TechnologyEntity();
                projectTechnologyJunctionEntity.Technology.Name = name;

                Projects.Add(projectTechnologyJunctionEntity);
            }
        }

        //public override string ToString()
        //{
        //    return $"Patch Technology entity identified by: name = '{_name}', Position = '{PositionId}'" +
        //        $" with values: Company name = '{CompanyName}', Position = '{Position}'";
        //}
    }
}
