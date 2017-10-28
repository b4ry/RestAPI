using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DivingApplication.Entities.Entities
{
    [Table("Experiences")]
    public class Experience : BaseEntity
    {
        [Required]
        [MaxLength(10)]
        public string CompanyName { get; set; }

        [Required]
        [MaxLength(10)]
        public string Position { get; set; }

        [ForeignKey("ExperienceId")]
        public IList<Project> Projects { get; set; }
    }
}
