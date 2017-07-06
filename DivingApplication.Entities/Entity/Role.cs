using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DivingApplication.Entities.Entity
{
    public class Role : IdentityRole
    {
        public string Description { get; set; }
    }
}
