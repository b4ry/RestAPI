using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PortfolioApplication.Entities.Entities
{
    [Table("Experiences")]
    public class ExperienceEntity : BaseEntity
    {
        [Required]
        [MaxLength(10)]
        public string CompanyName { get; set; }

        [Required]
        [MaxLength(10)]
        public string Position { get; set; }

        [ForeignKey("ExperienceId")]
        public IList<ProjectEntity> Projects { get; set; }
    }
}
