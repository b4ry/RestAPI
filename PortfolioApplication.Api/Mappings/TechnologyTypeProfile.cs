using AutoMapper;
using PortfolioApplication.Api.DataTransferObjects;
using PortfolioApplication.Entities.Entities;

namespace PortfolioApplication.Api.Mappings
{
    internal class TechnologyTypeProfile : Profile
    {
        internal TechnologyTypeProfile()
        {
            CreateMap<TechnologyTypeEntity, TechnologyTypeDto>().ReverseMap();
        }
    }
}
