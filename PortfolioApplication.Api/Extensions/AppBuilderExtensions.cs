using PortfolioApplication.Api.Mappings;
using Microsoft.AspNetCore.Builder;

namespace PortfolioApplication.Api.Extensions
{
    internal static class AppBuilderExtensions
    {
        internal static void UseAutoMapper(this IApplicationBuilder app)
        {
            AutoMapperConfiguration.Configure();
        }
    }
}
