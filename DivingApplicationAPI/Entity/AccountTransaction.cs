using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DivingApplicationAPI.Entity
{
    [Table("AccountTransactions")]
    public class AccountTransaction : BaseEntity
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public DateTimeOffset TransactionDate { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public double Amount { get; set; }
    }
}
