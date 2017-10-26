using AutoMapper;
using DivingApplication.Api.DataTransferObjects;
using DivingApplication.Entities.Entities;

namespace DivingApplication.Api.Mappings
{
    internal class DivingGearTypeProfile : Profile
    {
        public DivingGearTypeProfile()
        {
            CreateMap<DivingGearType, DivingGearTypeDto>().ReverseMap();
        }
    }
}
