using PortfolioApplication.Api.DataTransferObjects.Technology;

namespace PortfolioApplication.Api.DataTransferObjects.Project
{
    /// <summary>
    /// Data transfer object for technology associated with project
    /// </summary>
    public class ProjectTechnologyDto
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
