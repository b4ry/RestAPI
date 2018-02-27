using System.Collections.Generic;

namespace PortfolioApplication.Api.DataTransferObjects.Technologies
{
    public class PatchTechnologyDto : BaseDto
    {
        /// <summary>
        /// Name of the technology
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// CSS class for technology icon
        /// </summary>
        public string IconClass { get; set; }

        /// <summary>
        /// IDs of project-technology relations
        /// </summary>
        public IList<PatchProjectTechnologyJunctionDto> Projects { get; set; }

        /// <summary>
        /// Type of the technology
        /// </summary>
        public TechnologyTypeDto TechnologyType { get; set; }
    }
}
