using DivingApplicationAPI.Mappings;
using Microsoft.AspNetCore.Builder;

namespace DivingApplicationAPI.Extensions
{
    public static class AppBuilderExtension
    {
        public static void UseAutoMapper(this IApplicationBuilder app)
        {
            AutoMapperConfiguration.Configure();
        }
    }
}
