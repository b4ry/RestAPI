using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PortfolioApplication.Entities.Entities.JunctionEntities
{
    [Table("ProjectsTechnologies")]
    public class ProjectTechnologyJunctionEntity : BaseEntity
    {
        [Required]
        public int ProjectId { get; set; }

        [ForeignKey(nameof(ProjectId))]
        public ProjectEntity Project { get; set; }

        [Required]
        public int TechnologyId { get; set; }

        [ForeignKey(nameof(TechnologyId))]
        public TechnologyEntity Technology { get; set; }
    }
}
