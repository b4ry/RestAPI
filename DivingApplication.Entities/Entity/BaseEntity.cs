using System;
using System.ComponentModel.DataAnnotations;

namespace DivingApplication.Entities.Entity
{
    public class BaseEntity
    {
        [Key]
        public virtual Guid Id { set; get; }
    }
}
