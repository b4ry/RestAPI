using AutoMapper;
using PortfolioApplication.Api.CQRS.Commands.Technologies.Commands;
using PortfolioApplication.Api.DataTransferObjects.Technologies;
using PortfolioApplication.Entities.Entities;
using System.Linq;

namespace PortfolioApplication.Api.Mappings
{
    /// <summary>
    /// Mapping profile for Technology entity
    /// </summary>
    public class TechnologyProfile : Profile
    {
        /// <summary>
        /// TechnologyProfile constructor configuring mapping profile for Technology entity
        /// </summary>
        public TechnologyProfile()
        {
            CreateMap<TechnologyEntity, TechnologyDto>()
                .ForMember(dest => dest.Projects, opt => opt.MapFrom(src => src.Projects.Select(p => p.Project)
                .ToList()));
            CreateMap<TechnologyDto, TechnologyEntity>()
                .ForMember(dest => dest.TechnologyTypeId, opt => opt.Ignore());
            CreateMap<CreateTechnologyCommand, TechnologyEntity>()
                .ForMember(dest => dest.TechnologyTypeId, opt => opt.MapFrom(src => src.TechnologyTypeEnum));
        }
    }
}
