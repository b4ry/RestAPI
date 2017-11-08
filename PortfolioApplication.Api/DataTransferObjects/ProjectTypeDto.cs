using PortfolioApplication.Entities.Enums;

namespace PortfolioApplication.Api.DataTransferObjects
{
    /// <summary>
    /// Data transfer object for ProjectType entity
    /// </summary>
    public class ProjectTypeDto
    {
        /// <summary>
        /// ProjectType name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// ProjectType enumeration object
        /// </summary>
        public ProjectTypeEnum ProjectTypeEnum { get; set; }
    }
}
