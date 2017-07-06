using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DivingApplication.Entities.Entity
{
    [Table("DivingGears")]
    public class DivingGear : BaseEntity
    {
        [Required]
        [MaxLength(10)]
        public string Name { get; set; }

        [MaxLength(10)]
        public string Brand { get; set; }

        [MaxLength(10)]
        public string Model { get; set; }

        [Required]
        public Guid DivingGearTypeId { get; set; }

        [ForeignKey(nameof(DivingGearTypeId))]
        public DivingGearType DivingGearType { get; set; }
    }
}
