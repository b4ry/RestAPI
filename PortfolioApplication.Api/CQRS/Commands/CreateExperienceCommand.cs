namespace PortfolioApplication.Api.CQRS.Commands
{
    public class CreateExperienceCommand : ICreateExperienceCommand
    {
        public readonly string CompanyName;
        public readonly string Position;

        public CreateExperienceCommand(string companyName, string position)
        {
            CompanyName = companyName;
            Position = position;
        }

        public override string ToString()
        {
            return $"Create Experience entity: Company name = '{CompanyName}', Position = '{Position}'";
        }
    }
}
