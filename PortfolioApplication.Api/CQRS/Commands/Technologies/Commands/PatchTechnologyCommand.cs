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

        public PatchTechnologyCommand(string name, IList<ProjectTechnologyJunctionEntity> projects, TechnologyTypeEnum technologyTypeEnum)
        {
            Name = name;
            Projects = projects;
            TechnologyTypeEnum = technologyTypeEnum;
        }

        //public override string ToString()
        //{
        //    return $"Patch Technology entity identified by: name = '{_name}', Position = '{PositionId}'" +
        //        $" with values: Company name = '{CompanyName}', Position = '{Position}'";
        //}
    }
}
