using AutoMapper;
using CrankBankAPI.DataTransferObjects;
using CrankBankAPI.Entity;

namespace CrankBankAPI.Mappings
{
    internal class AccountDetailProfile : Profile
    {
        public AccountDetailProfile()
        {
            CreateMap<AccountDetail, AccountDetailDto>();
        }
    }
}