using PortfolioApplication.Api.DataTransferObjects.Project;
using System;

namespace PortfolioApplication.Api.DataTransferObjects.Technology
{
    /// <summary>
    /// Data transfer object for project associated with technology
    /// </summary>
    public class TechnologyProjectDto : BaseDto
    {
        /// <summary>
        /// Project name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Project description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Date of start
        /// </summary>
        public DateTime StartTime { get; set; }

        /// <summary>
        /// Date of end
        /// </summary>
        public DateTime EndTime { get; set; }

        /// <summary>
        /// Type of the project
        /// </summary>
        public ProjectTypeDto ProjectType { get; set; }
    }
}
