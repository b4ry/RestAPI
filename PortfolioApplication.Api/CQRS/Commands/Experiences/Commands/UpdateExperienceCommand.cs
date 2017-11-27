namespace PortfolioApplication.Api.CQRS.Commands.Experiences.Commands
{
    public class UpdateExperienceCommand : IUpdateExperienceCommand
    {
        public string CompanyNameId { get; }
        public string PositionId { get; }
        public string CompanyName { get; }
        public string Position { get; }

        public UpdateExperienceCommand(string companyNameId, string positionId, string companyName, string position)
        {
            CompanyNameId = companyNameId;
            PositionId = positionId;
            CompanyName = companyName;
            Position = position;
        }

        public override string ToString()
        {
            return $"Update Experience entity identified by: Company name = '{CompanyNameId}', Position = '{PositionId}'" +
                $" with values: Company name = '{CompanyName}', Position = '{Position}'";
        }
    }
}
