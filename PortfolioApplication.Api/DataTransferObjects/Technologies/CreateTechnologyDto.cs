using PortfolioApplication.Entities.Enums;

namespace PortfolioApplication.Api.DataTransferObjects.Technologies
{
    /// <summary>
    /// Data transfer object for creating Technology entity
    /// </summary>
    public class CreateTechnologyDto
    {
        /// <summary>
        /// Name of the technology
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Type of the technology
        /// </summary>
        public TechnologyTypeEnum TechnologyTypeEnum { get; set; }

        /// <summary>
        /// URL path to the technology icon
        /// </summary>
        public string IconUrl { get; set; }
    }
}
