using System.ComponentModel.DataAnnotations;

namespace DivingApplication.Entities.Entities
{
    public class BaseEntity
    {
        [Key]
        public int Id { set; get; }
    }
}
