using DivingApplication.Entities.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DivingApplication.Services.CQRS.Queries
{
    public interface IDivingGearQuery : IQuery
    {
        Task<IList<DivingGear>> GetDivingGearsAsync();
    }
}
