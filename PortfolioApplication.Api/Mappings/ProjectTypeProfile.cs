using AutoMapper;
using PortfolioApplication.Api.DataTransferObjects;
using PortfolioApplication.Entities.Entities;

namespace PortfolioApplication.Api.Mappings
{
    internal class ProjectTypeProfile : Profile
    {
        internal ProjectTypeProfile()
        {
            CreateMap<ProjectTypeEntity, ProjectTypeDto>().ReverseMap();
        }
    }
}
