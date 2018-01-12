using System.Collections.Generic;

namespace PortfolioApplication.Api.DataTransferObjects.Technologies
{
    /// <summary>
    /// Data transfer object for Technology entity
    /// </summary>
    public class TechnologyDto : BaseDto
    {
        /// <summary>
        /// Name of the technology
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Associated projects
        /// </summary>
        public IList<TechnologyProjectDto> Projects { get; set; }

        /// <summary>
        /// Type of the technology
        /// </summary>
        public TechnologyTypeDto TechnologyType { get; set; }

        /// <summary>
        /// CSS class for technology icon
        /// </summary>
        public string IconClass { get; set; }
    }
}
