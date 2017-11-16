using System;
using System.Collections.Generic;

namespace PortfolioApplication.Api.DataTransferObjects.Project
{
    /// <summary>
    /// Data transfer object for Project entity
    /// </summary>
    public class ProjectDto : BaseDto
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
        /// Associated technologies
        /// </summary>
        public IList<ProjectTechnologyDto> Technologies { get; set; }

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
