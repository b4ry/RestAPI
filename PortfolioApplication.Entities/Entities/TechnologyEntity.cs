using PortfolioApplication.Entities.Entities.JunctionEntities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PortfolioApplication.Entities.Entities
{
    [Table("Technologies")]
    public class TechnologyEntity : BaseEntity
    {
        [Required]
        public string Name { get; set; }
        
        [ForeignKey("TechnologyId")]
        public IList<ProjectTechnologyJunctionEntity> Projects { get; set; }

        [Required]
        public int TechnologyTypeId { get; set; }

        [ForeignKey(nameof(TechnologyTypeId))]
        public TechnologyTypeEntity TechnologyType { get; set; }
    }
}
