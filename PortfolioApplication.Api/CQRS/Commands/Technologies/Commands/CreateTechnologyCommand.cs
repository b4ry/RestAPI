using PortfolioApplication.Entities.Enums;

namespace PortfolioApplication.Api.CQRS.Commands.Technologies.Commands
{
    public class CreateTechnologyCommand : ICreateTechnologyCommand
    {
        public string Name { get; }
        public TechnologyTypeEnum TechnologyTypeEnum { get; }
        public string IconUrl { get; }

        public CreateTechnologyCommand(string name, TechnologyTypeEnum technologyTypeEnum, string iconUrl)
        {
            Name = name;
            TechnologyTypeEnum = technologyTypeEnum;
            IconUrl = iconUrl;
        }

        public override string ToString()
        {
            return $"Create Technology entity: Company name = '{Name}', TechnologyType = '{TechnologyTypeEnum}', IconUrl = '{IconUrl}'";
        }
    }
}
