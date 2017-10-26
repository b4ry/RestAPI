using System.ComponentModel.DataAnnotations.Schema;

namespace DivingApplication.Entities.Entities
{
    [Table("Technologies")]
    public class Technology : BaseEntity
    {
        public string Name { get; set; }
    }
}
