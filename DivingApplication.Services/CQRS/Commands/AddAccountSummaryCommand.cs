using DivingApplication.Entities.Entity;

namespace DivingApplication.Services.CQRS.Commands
{
    public class AddAccountSummaryCommand : ICommand
    {

        public string AccountNumber { get; set; }

        public AccountTypeEnum Type { get; set; }

        public string Name { get; set; }

        public double Balance { get; set; }
    }
}
