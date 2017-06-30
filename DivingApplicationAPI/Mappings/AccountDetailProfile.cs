using AutoMapper;
using DivingApplicationAPI.DataTransferObjects;
using DivingApplicationAPI.Entity;

namespace DivingApplicationAPI.Mappings
{
    internal class AccountDetailProfile : Profile
    {
        public AccountDetailProfile()
        {
            CreateMap<AccountDetail, AccountDetailDto>();
        }
    }
}