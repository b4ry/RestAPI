using AutoMapper;
using PortfolioApplication.Api.DataTransferObjects;
using PortfolioApplication.Entities.Entities;

namespace PortfolioApplication.Api.Mappings
{
    /// <summary>
    /// Mapping profile for ProjectType entity
    /// </summary>
    internal class ProjectTypeProfile : Profile
    {
        /// <summary>
        /// ProjectTypeProfile constructor
        /// </summary>
        public ProjectTypeProfile()
        {
            CreateMap<ProjectTypeEntity, ProjectTypeDto>().ReverseMap();
        }
    }
}
