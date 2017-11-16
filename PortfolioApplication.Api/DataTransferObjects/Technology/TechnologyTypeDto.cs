using PortfolioApplication.Entities.Enums;

namespace PortfolioApplication.Api.DataTransferObjects.Technology
{
    /// <summary>
    /// Data transfer object for TechnologyType entity
    /// </summary>
    public class TechnologyTypeDto : BaseDto
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
