using DivingApplication.Entities.Entity;
using DivingApplication.Services.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DivingApplication.Services.CQRS.Queries
{
    public class DivingGearQuery : IDivingGearQuery
    {
        protected IDatabaseSet DatabaseSet { get; }
        protected DbSet<DivingGear> DivingGearSet { get; }
        //protected DbSet<AccountDetail> AccountDetailSet { get; }

        public DivingGearQuery(IDatabaseSet databaseSet)
        {
            DatabaseSet = databaseSet;
            DivingGearSet = DatabaseSet.Set<DivingGear>();
        }

        public async Task<IList<DivingGear>> GetDivingGearsAsync()
        {
            return await DivingGearSet.ToListAsync();
        }

        //public async Task<AccountDetail> GetAccountDetailByIdAsync(int id)
        //{
        //    return await AccountDetailSet.Include(x => x.AccountTransactions).SingleAsync(y => y.AccountSummary.Id == id);
        //}
    }
}
