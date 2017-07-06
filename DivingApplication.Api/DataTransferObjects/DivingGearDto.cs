namespace DivingApplication.Api.DataTransferObjects
{
    /// <summary>
    /// Diving gear data transfer object used in requests to API
    /// </summary>
    public class DivingGearDto
    {
        /// <summary>
        /// Indicates name of the diving gear
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Manufacturer's name
        /// </summary>
        public string Brand { get; set; }

        /// <summary>
        /// Specific model of the diving gear
        /// </summary>
        public string Model { get; set; }

        /// <summary>
        /// Specific type of the diving gear
        /// </summary>
        public DivingGearTypeDto DivingGearType { get; set; }
    }
}
