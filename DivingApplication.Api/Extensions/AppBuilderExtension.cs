using DivingApplication.Api.Mappings;
using Microsoft.AspNetCore.Builder;

namespace DivingApplication.Api.Extensions
{
    public static class AppBuilderExtension
    {
        public static void UseAutoMapper(this IApplicationBuilder app)
        {
            AutoMapperConfiguration.Configure();
        }
    }
}
