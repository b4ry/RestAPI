using AutoMapper;
using PortfolioApplication.Api.CQRS.Commands.Technologies.Commands;
using PortfolioApplication.Api.DataTransferObjects.Technologies;
using PortfolioApplication.Api.Mappings.Resolvers;
using PortfolioApplication.Entities.Entities;
using PortfolioApplication.Services.DatabaseContexts;
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
        public TechnologyProfile(IDatabaseSet databaseSet)
        {
            CreateMap<TechnologyEntity, TechnologyDto>()
                .ForMember(dest => dest.Projects, opt => opt.MapFrom(src => src.Projects.Select(p => p.Project)
                .ToList()));
            CreateMap<TechnologyDto, TechnologyEntity>()
                .ForMember(dest => dest.TechnologyTypeId, opt => opt.Ignore())
                .ForMember(dest => dest.Projects, opt => opt.ResolveUsing(new ProjectResolver(databaseSet)));
            CreateMap<CreateTechnologyCommand, TechnologyEntity>()
                .ForMember(dest => dest.TechnologyTypeId, opt => opt.MapFrom(src => src.TechnologyTypeEnum));
        }
    }
}
