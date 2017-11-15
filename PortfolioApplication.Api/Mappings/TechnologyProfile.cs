using AutoMapper;
using PortfolioApplication.Api.DataTransferObjects;
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
            CreateMap<TechnologyEntity, TechnologyDto>().ForMember(x => x.Projects, opt => opt.MapFrom(src => src.Projects.Select(p => p.Project).ToList()));
            CreateMap<TechnologyDto, TechnologyEntity>().ForMember(x => x.TechnologyTypeId, opt => opt.Ignore());
        }
    }
}
