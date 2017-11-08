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
        /// TechnologyType enumeration object
        /// </summary>
        [Required]
        public TechnologyTypeEnum TechnologyTypeEnum { get; set; }
    }
}
