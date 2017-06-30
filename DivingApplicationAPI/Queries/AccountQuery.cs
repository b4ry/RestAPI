using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DivingApplicationAPI.Entity;
using DivingApplicationAPI.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace DivingApplicationAPI.Queries
{
    public class AccountQuery : IAccountQuery
    {
        protected IDatabaseSet DatabaseSet { get; }
        protected DbSet<AccountSummary> AccountSummarySet { get; }
        protected DbSet<AccountDetail> AccountDetailSet { get; }

        public AccountQuery(IDatabaseSet databaseSet)
        {
            DatabaseSet = databaseSet;
            AccountSummarySet = DatabaseSet.Set<AccountSummary>();
            AccountDetailSet = DatabaseSet.Set<AccountDetail>();
        }

        public async Task<IList<AccountSummary>> GetAccountSummariesAsync()
        {
            return await AccountSummarySet.ToListAsync();
        }

        public async Task<AccountDetail> GetAccountDetailByIdAsync(int id)
        {
            return await AccountDetailSet.Include(x => x.AccountTransactions).SingleAsync(y => y.AccountSummary.Id == id);
        }
    }
}
