using DivingApplication.Entities.Entity;
using DivingApplication.Services.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace DivingApplication.Services.CQRS.Commands
{
    public class AddAccountSummaryHandler : IHandleCommand<AddAccountSummaryCommand>
    {
        protected IDatabaseSet DatabaseSet { get; }
        protected DbSet<AccountSummary> AccountSummarySet { get; }

        public AddAccountSummaryHandler(IDatabaseSet databaseSet)
        {
            DatabaseSet = databaseSet;
            AccountSummarySet = DatabaseSet.Set<AccountSummary>();
        }

        public void Handle(AddAccountSummaryCommand command)
        {
            AccountSummarySet.Add(new AccountSummary()
            {
                AccountNumber = command.AccountNumber,
                Balance = command.Balance,
                Name = command.Name,
                Type = command.Type
            }
            );
        }
    }
}
