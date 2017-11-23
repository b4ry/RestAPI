using AutoMapper;
using PortfolioApplication.Api.DataTransferObjects.Projects;
using PortfolioApplication.Entities.Entities;
using System.Linq;

namespace PortfolioApplication.Api.Mappings
{
    /// <summary>
    /// Mapping profile for Experience entity
    /// </summary>
    public class ProjectProfile : Profile
    {
        /// <summary>
        /// ExperienceProfile constructor configuring mapping profile for Experience entity
        /// </summary>
        public ProjectProfile()
        {
            CreateMap<ProjectEntity, ProjectDto>().ForMember(x => x.Technologies, opt => opt.MapFrom(src => src.Technologies.Select(t => t.Technology).ToList()));
            CreateMap<ProjectDto, ProjectEntity>().ForMember(x => x.ProjectTypeId, opt => opt.Ignore());
        }
    }
}
