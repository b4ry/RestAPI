using PortfolioApplication.Api.Mappings;
using Microsoft.AspNetCore.Builder;

namespace PortfolioApplication.Api.Extensions
{
    public static class AppBuilderExtensions
    {
        public static void UseAutoMapper(this IApplicationBuilder app)
        {
            AutoMapperConfiguration.Configure();
        }
    }
}
