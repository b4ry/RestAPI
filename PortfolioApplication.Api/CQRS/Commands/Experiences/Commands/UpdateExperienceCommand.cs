namespace PortfolioApplication.Api.CQRS.Commands.Experiences.Commands
{
    public class UpdateExperienceCommand : IUpdateExperienceCommand
    {
        public string CompanyName { get; }
        public string Position { get; }

        public UpdateExperienceCommand(string companyName, string position)
        {
            CompanyName = companyName;
            Position = position;
        }

        //public override string ToString()
        //{
        //    return $"Update Experience entity identified by: Company name = '{CompanyNameId}', Position = '{PositionId}'" +
        //        $" with values: Company name = '{CompanyName}', Position = '{Position}'";
        //}
    }
}
