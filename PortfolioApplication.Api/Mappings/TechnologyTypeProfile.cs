using AutoMapper;
using PortfolioApplication.Api.DataTransferObjects.Technology;
using PortfolioApplication.Entities.Entities;

namespace PortfolioApplication.Api.Mappings
{
    /// <summary>
    /// Mapping profile for TechnologyType entity
    /// </summary>
    public class TechnologyTypeProfile : Profile
    {
        /// <summary>
        /// TechnologyTypeProfile constructor configuring mapping mapping profile for TechnologyType entity
        /// </summary>
        public TechnologyTypeProfile()
        {
            CreateMap<TechnologyTypeEntity, TechnologyTypeDto>().ReverseMap();
        }
    }
}
