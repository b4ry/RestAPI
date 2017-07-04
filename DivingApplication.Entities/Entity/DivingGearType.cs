using DivingApplication.Entities.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DivingApplication.Entities.Entity
{
    [Table("DivingGearTypes")]
    public class DivingGearType : BaseEntity
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        public DivingGearTypeEnum DivingGearTypeEnum { get; set; }   
    }
}
