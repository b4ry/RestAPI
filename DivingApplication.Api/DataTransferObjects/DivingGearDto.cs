using DivingApplication.Entities.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DivingApplication.Api.DataTransferObjects
{
    public class DivingGearDto
    {
        public string Name { get; set; }

        public DivingGearTypeDto DivingGearType { get; set; }
    }
}
