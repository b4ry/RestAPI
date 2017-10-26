using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DivingApplication.Entities.Entities
{
    [Table("Projects")]
    public class Project : BaseEntity
    {
        [Required]
        [MaxLength(20)]
        public string Name { get; set; }
        
        [Required]
        public string Description { get; set; }

        [Required]
        public List<Technology> Technologies { get; set; }

        [Required]
        public DateTime StartTime { get; set; }

        [Required]
        public DateTime EndTime { get; set; }
    }
}
