using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DivingApplication.Entities.Entities;
using DivingApplication.Services.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace DivingApplication.Services.CQRS.Queries
{
    public class DivingGearTypeQuery : IDivingGearTypeQuery
    {
        protected IDatabaseSet DatabaseSet { get; }
        protected DbSet<DivingGearType> DivingGearTypeSet { get; }

        public DivingGearTypeQuery(IDatabaseSet databaseSet)
        {
            DatabaseSet = databaseSet;
            DivingGearTypeSet = DatabaseSet.Set<DivingGearType>();
        }

        public async Task<IList<DivingGearType>> GetDivingGearTypesAsync()
        {
            return await DivingGearTypeSet.ToListAsync();
        }
    }
}
