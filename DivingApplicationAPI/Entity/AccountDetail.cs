﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CrankBankAPI.Entity
{
    [Table("AccountDetails")]
    public class AccountDetail : BaseEntity
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public AccountSummary AccountSummary { get; set; }
        [Required]
        public IList<AccountTransaction> AccountTransactions { get; set; }
    }
}