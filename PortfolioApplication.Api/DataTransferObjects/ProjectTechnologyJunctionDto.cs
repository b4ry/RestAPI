namespace PortfolioApplication.Api.DataTransferObjects
{
    /// <summary>
    /// Data transfer object for ProjectTechnologyJunction entity
    /// </summary>
    public class ProjectTechnologyJunctionDto
    {
        /// <summary>
        /// Project associated to the technology
        /// </summary>
        public ProjectDto Project { get; set; }

        /// <summary>
        /// Technology associated to the project
        /// </summary>
        public TechnologyDto Technology { get; set; }
    }
}
