using DivingApplication.Entities.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DivingApplication.Services.CQRS.Queries
{
    public interface IAccountQuery : IQuery
    {
        Task<IList<AccountSummary>> GetAccountSummariesAsync();
        Task<AccountDetail> GetAccountDetailByIdAsync(int id);
    }
}
