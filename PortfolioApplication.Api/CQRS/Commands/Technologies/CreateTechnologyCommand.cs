using PortfolioApplication.Entities.Enums;

namespace PortfolioApplication.Api.CQRS.Commands.Technologies
{
    public class CreateTechnologyCommand : ICreateTechnologyCommand
    {
        public string Name { get; }
        public TechnologyTypeEnum TechnologyTypeEnum { get; }

        public CreateTechnologyCommand(string name, TechnologyTypeEnum technologyTypeEnum)
        {
            Name = name;
            TechnologyTypeEnum = technologyTypeEnum;
        }

        public override string ToString()
        {
            return $"Create Technology entity: Company name = '{Name}', TechnologyType = '{TechnologyTypeEnum}'";
        }
    }
}
