using CrankBankAPI.DatabaseContext;
using CrankBankAPI.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrankBankAPI.Commands
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
