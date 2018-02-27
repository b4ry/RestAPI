using AutoMapper;
using PortfolioApplication.Api.DataTransferObjects;
using PortfolioApplication.Entities.Entities.JunctionEntities;

namespace PortfolioApplication.Api.Mappings
{
    /// <summary>
    /// Mapping profile for ProjectTechnologyJunction entity
    /// </summary>
    public class ProjectTechnologyJunctionProfile : Profile
    {
        /// <summary>
        /// ProjectTechnologyJunctionProfile constructor configuring mapping profile for ProjectTechnologyJunction entity
        /// </summary>
        public ProjectTechnologyJunctionProfile()
        {
            CreateMap<ProjectTechnologyJunctionEntity, ProjectTechnologyJunctionDto>().ReverseMap();
            CreateMap<ProjectTechnologyJunctionEntity, PatchProjectTechnologyJunctionDto>().ReverseMap();
        }
    }
}
