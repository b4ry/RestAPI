using PortfolioApplication.Entities.Enums;
using System.ComponentModel.DataAnnotations;

namespace PortfolioApplication.Api.DataTransferObjects
{
    /// <summary>
    /// Data transfer object for TechnologyType entity
    /// </summary>
    public class TechnologyTypeDto 
    {
        /// <summary>
        /// TechnologyType name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// TechnologyType enumeration object
        /// </summary>
        public TechnologyTypeEnum TechnologyTypeEnum { get; set; }
    }
}
