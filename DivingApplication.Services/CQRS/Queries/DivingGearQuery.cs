using DivingApplication.Entities.Entities;
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

        public DivingGearQuery(IDatabaseSet databaseSet)
        {
            DatabaseSet = databaseSet;
            DivingGearSet = DatabaseSet.Set<DivingGear>();
        }

        public async Task<IList<DivingGear>> GetDivingGearsAsync()
        {
            return await DivingGearSet.ToListAsync();
        }
    }
}
