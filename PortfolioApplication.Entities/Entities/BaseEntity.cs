using System.ComponentModel.DataAnnotations;

namespace PortfolioApplication.Entities.Entities
{
    public class BaseEntity
    {
        [Key]
        public int Id { set; get; }
    }
}
