using AutoMapper;
using PortfolioApplication.Api.DataTransferObjects;
using PortfolioApplication.Entities.Entities;

namespace PortfolioApplication.Api.Mappings
{
    /// <summary>
    /// Mapping profile for TechnologyType entity
    /// </summary>
    internal class TechnologyTypeProfile : Profile
    {
        /// <summary>
        /// TechnologyTypeProfile constructor
        /// </summary>
        public TechnologyTypeProfile()
        {
            CreateMap<TechnologyTypeEntity, TechnologyTypeDto>().ReverseMap();
        }
    }
}
