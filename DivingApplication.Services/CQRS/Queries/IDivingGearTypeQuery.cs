using DivingApplication.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DivingApplication.Services.CQRS.Queries
{
    public interface IDivingGearTypeQuery
    {
        Task<IList<DivingGearType>> GetDivingGearTypesAsync();
    }
}
