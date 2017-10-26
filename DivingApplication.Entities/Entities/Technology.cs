using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DivingApplication.Entities.Entities
{
    [Table("Technologies")]
    public class Technology : BaseEntity
    {
        [Required]
        public string Name { get; set; }

        public List<Project> Projects { get; set; }
    }
}
