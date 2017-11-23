namespace PortfolioApplication.Api.CQRS.Commands.Experiences
{
    public class DeleteExperienceCommand : IDeleteExperienceCommand
    {
        public string CompanyName { get; }
        public string Position { get; }

        public DeleteExperienceCommand(string companyName, string position)
        {
            CompanyName = companyName;
            Position = position;
        }

        public override string ToString()
        {
            return $"Delete Experience entity: Company name = '{CompanyName}', Position = '{Position}'";
        }
    }
}
