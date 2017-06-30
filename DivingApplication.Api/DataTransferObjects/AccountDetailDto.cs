using DivingApplication.Entities.Entity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DivingApplication.Api.DataTransferObjects
{
    public class AccountDetailDto
    {
        [Required]
        public AccountSummary AccountSummary { get; set; }
        [Required]
        public IList<AccountTransaction> AccountTransactions { get; set; }
    }
}
