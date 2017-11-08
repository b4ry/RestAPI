using AutoMapper;
using PortfolioApplication.Api.DataTransferObjects;
using PortfolioApplication.Entities.Entities;

namespace PortfolioApplication.Api.Mappings
{
    internal class ProjectTypeProfile : Profile
    {
        public ProjectTypeProfile()
        {
            CreateMap<ProjectTypeEntity, ProjectTypeDto>().ReverseMap();
        }
    }
}
