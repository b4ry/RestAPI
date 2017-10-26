using AutoMapper;
using DivingApplication.Api.DataTransferObjects;
using DivingApplication.Entities.Entities;

namespace DivingApplication.Api.Mappings
{
    internal class DivingGearProfile : Profile
    {
        public DivingGearProfile()
        {
            CreateMap<DivingGearDto, DivingGear>().ReverseMap();
        }
    }
}