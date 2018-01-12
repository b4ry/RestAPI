using PortfolioApplication.Entities.Enums;

namespace PortfolioApplication.Api.CQRS.Commands.Technologies.Commands
{
    public class CreateTechnologyCommand : ICreateTechnologyCommand
    {
        public string Name { get; }
        public TechnologyTypeEnum TechnologyTypeEnum { get; }
        public string IconClass { get; }

        public CreateTechnologyCommand(string name, TechnologyTypeEnum technologyTypeEnum, string iconClass)
        {
            Name = name;
            TechnologyTypeEnum = technologyTypeEnum;
            IconClass = iconClass;
        }

        public override string ToString()
        {
            return $"Create Technology entity: Company name = '{Name}', TechnologyType = '{TechnologyTypeEnum}', IconClass = '{IconClass}'";
        }
    }
}
