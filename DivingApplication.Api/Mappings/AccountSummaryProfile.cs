using AutoMapper;
using DivingApplication.Api.DataTransferObjects;
using DivingApplication.Entities.Entity;

namespace DivingApplication.Api.Mappings
{
    internal class AccountSummaryProfile : Profile
    {
        public AccountSummaryProfile()
        {
            CreateMap<AccountSummary, AccountSummaryDto>();
        }
    }
}