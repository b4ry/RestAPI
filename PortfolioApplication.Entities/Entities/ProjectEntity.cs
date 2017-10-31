using PortfolioApplication.Entities.Entities.JunctionEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PortfolioApplication.Entities.Entities
{
    [Table("Projects")]
    public class ProjectEntity : BaseEntity
    {
        [Required]
        [MaxLength(20)]
        public string Name { get; set; }
        
        [Required]
        public string Description { get; set; }

        [ForeignKey("ProjectId")]
        public IList<ProjectTechnologyJunctionEntity> ProjectsTechnologies { get; set; }

        [Required]
        public DateTime StartTime { get; set; }

        [Required]
        public DateTime EndTime { get; set; }

        [Required]
        public int ProjectTypeId { get; set; }

        [ForeignKey(nameof(ProjectTypeId))]
        public ProjectTypeEntity ProjectType { get; set; }
    }
}
