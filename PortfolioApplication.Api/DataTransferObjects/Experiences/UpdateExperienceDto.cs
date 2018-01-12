namespace PortfolioApplication.Api.DataTransferObjects.Experiences
{
    /// <summary>
    /// Data transfer object for updating Experience entity
    /// </summary>
    public class UpdateExperienceDto
    {
        /// <summary>
        /// Name of the company
        /// </summary>
        public string CompanyName { get; set; }

        /// <summary>
        /// Position name
        /// </summary>
        public string Position { get; set; }
    }
}
