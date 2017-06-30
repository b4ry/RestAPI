using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CrankBankAPI.Entity
{
    [Table("AccountSummaries")]
    public class AccountSummary : BaseEntity
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string AccountNumber { get; set; }
        [Required]
        public AccountTypeEnum Type { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public double Balance { get; set; }
    }
}
