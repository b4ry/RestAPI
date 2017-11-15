using AutoMapper;
using PortfolioApplication.Api.DataTransferObjects;
using PortfolioApplication.Entities.Entities;

namespace PortfolioApplication.Api.Mappings
{
    /// <summary>
    /// Mapping profile for Experience entity
    /// </summary>
    public class ExperienceProfile : Profile
    {
        /// <summary>
        /// ExperienceProfile constructor configuring mapping profile for Experience entity
        /// </summary>
        public ExperienceProfile()
        {
            CreateMap<ExperienceEntity, ExperienceDto>().ReverseMap();
        }
    }
}
