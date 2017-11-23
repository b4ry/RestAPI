using PortfolioApplication.Api.DataTransferObjects.Technologies;

namespace PortfolioApplication.Api.DataTransferObjects.Projects
{
    /// <summary>
    /// Data transfer object for technology associated with project
    /// </summary>
    public class ProjectTechnologyDto : BaseDto
    {
        /// <summary>
        /// Name of the technology
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Type of the technology
        /// </summary>
        public TechnologyTypeDto TechnologyType { get; set; }
    }
}
