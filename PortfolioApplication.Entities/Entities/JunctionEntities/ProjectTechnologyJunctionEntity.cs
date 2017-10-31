using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PortfolioApplication.Entities.Entities.JunctionEntities
{
    [Table("ProjectsTechnologies")]
    public class ProjectTechnologyJunctionEntity : BaseEntity
    {
        [Required]
        public int ProjectId { get; set; }

        [Required]
        public int TechnologyId { get; set; }
    }
}
