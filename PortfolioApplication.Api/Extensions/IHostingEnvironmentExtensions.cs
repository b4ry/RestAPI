using Microsoft.AspNetCore.Hosting;

namespace PortfolioApplication.Api.Extensions
{
    internal static class IHostingEnvironmentExtensions
    {
        private const string developmentEnvironment = "Development";

        internal static bool IsDevelopmentModeOn(this IHostingEnvironment hostingEnvironment)
        {
            if(hostingEnvironment.EnvironmentName.Contains(developmentEnvironment))
            {
                return true;
            }

            return false;
        }
    }
}
