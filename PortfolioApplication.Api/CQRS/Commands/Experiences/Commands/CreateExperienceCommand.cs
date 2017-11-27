namespace PortfolioApplication.Api.CQRS.Commands.Experiences.Commands
{
    public class CreateExperienceCommand : ICreateExperienceCommand
    {
        public string CompanyName { get; }
        public string Position { get; }

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
