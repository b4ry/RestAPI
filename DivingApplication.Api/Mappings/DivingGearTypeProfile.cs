using AutoMapper;
using PortfolioApplication.Api.DataTransferObjects;
using PortfolioApplication.Entities.Entities;

namespace PortfolioApplication.Api.Mappings
{
    internal class DivingGearTypeProfile : Profile
    {
        public DivingGearTypeProfile()
        {
            CreateMap<DivingGearType, DivingGearTypeDto>().ReverseMap();
        }
    }
}
