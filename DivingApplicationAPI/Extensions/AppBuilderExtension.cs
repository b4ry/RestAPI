using CrankBankAPI.Mappings;
using Microsoft.AspNetCore.Builder;

namespace CrankBankAPI.Extensions
{
    public static class AppBuilderExtension
    {
        public static void UseAutoMapper(this IApplicationBuilder app)
        {
            AutoMapperConfiguration.Configure();
        }
    }
}
