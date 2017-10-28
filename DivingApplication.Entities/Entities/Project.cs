﻿using DivingApplication.Entities.Entities.JunctionEntities;
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

        [ForeignKey("ProjectId")]
        public IList<ProjectTechnology> ProjectsTechnologies { get; set; }

        [Required]
        public DateTime StartTime { get; set; }

        [Required]
        public DateTime EndTime { get; set; }
    }
}
