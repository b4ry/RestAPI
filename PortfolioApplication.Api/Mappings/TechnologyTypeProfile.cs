using AutoMapper;
using PortfolioApplication.Api.DataTransferObjects;
using PortfolioApplication.Entities.Entities;

namespace PortfolioApplication.Api.Mappings
{
    internal class TechnologyTypeProfile : Profile
    {
        public TechnologyTypeProfile()
        {
            CreateMap<TechnologyTypeEntity, TechnologyTypeDto>().ReverseMap();
        }
    }
}
