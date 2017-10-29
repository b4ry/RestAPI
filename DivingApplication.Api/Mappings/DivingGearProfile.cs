using AutoMapper;
using PortfolioApplication.Api.DataTransferObjects;
using PortfolioApplication.Entities.Entities;

namespace PortfolioApplication.Api.Mappings
{
    internal class DivingGearProfile : Profile
    {
        public DivingGearProfile()
        {
            CreateMap<DivingGearDto, DivingGear>().ReverseMap();
        }
    }
}