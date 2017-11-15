using System.Collections.Generic;

namespace PortfolioApplication.Api.DataTransferObjects
{
    /// <summary>
    /// Data transfer object for Technology entity
    /// </summary>
    public class TechnologyDto
    {
        /// <summary>
        /// Name of the technology
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Associated projects
        /// </summary>
        public IList<ProjectDto> Projects { get; set; }

        /// <summary>
        /// Type of the technology
        /// </summary>
        public TechnologyTypeDto TechnologyType { get; set; }
    }
}
