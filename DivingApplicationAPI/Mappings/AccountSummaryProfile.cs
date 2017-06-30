using AutoMapper;
using CrankBankAPI.DataTransferObjects;
using CrankBankAPI.Entity;

namespace CrankBankAPI.Mappings
{
    internal class AccountSummaryProfile : Profile
    {
        public AccountSummaryProfile()
        {
            CreateMap<AccountSummary, AccountSummaryDto>();
        }
    }
}