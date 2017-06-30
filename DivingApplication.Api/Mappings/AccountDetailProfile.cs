using AutoMapper;
using DivingApplication.Api.DataTransferObjects;
using DivingApplication.Entities.Entity;

namespace DivingApplication.Api.Mappings
{
    internal class AccountDetailProfile : Profile
    {
        public AccountDetailProfile()
        {
            CreateMap<AccountDetail, AccountDetailDto>();
        }
    }
}