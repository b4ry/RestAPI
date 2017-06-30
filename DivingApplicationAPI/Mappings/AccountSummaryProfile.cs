using AutoMapper;
using DivingApplicationAPI.DataTransferObjects;
using DivingApplicationAPI.Entity;

namespace DivingApplicationAPI.Mappings
{
    internal class AccountSummaryProfile : Profile
    {
        public AccountSummaryProfile()
        {
            CreateMap<AccountSummary, AccountSummaryDto>();
        }
    }
}