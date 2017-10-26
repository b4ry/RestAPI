using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DivingApplication.Entities.Entities
{
    public class Experience : BaseEntity
    {
        [Required]
        [MaxLength(10)]
        public string CompanyName { get; set; }

        [Required]
        [MaxLength(10)]
        public string Position { get; set; }

        [Required]
        public List<Project> Projects { get; set; }
    }
}
