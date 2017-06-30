using DivingApplicationAPI.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DivingApplicationAPI.Queries
{
    public interface IAccountQuery : IQuery
    {
        Task<IList<AccountSummary>> GetAccountSummariesAsync();
        Task<AccountDetail> GetAccountDetailByIdAsync(int id);
    }
}
