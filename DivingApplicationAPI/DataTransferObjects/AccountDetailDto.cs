using DivingApplicationAPI.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DivingApplicationAPI.DataTransferObjects
{
    public class AccountDetailDto
    {
        [Required]
        public AccountSummary AccountSummary { get; set; }
        [Required]
        public IList<AccountTransaction> AccountTransactions { get; set; }
    }
}
