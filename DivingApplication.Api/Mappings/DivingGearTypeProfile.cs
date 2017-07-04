using AutoMapper;
using DivingApplication.Api.DataTransferObjects;
using DivingApplication.Entities.Entity;

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
