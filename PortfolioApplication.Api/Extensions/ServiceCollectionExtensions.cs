using Autofac;
using Autofac.Extensions.DependencyInjection;
using PortfolioApplication.Api.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

namespace PortfolioApplication.Api.Extensions
{
    /// <summary>
    /// Extension class for ServiceCollection
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Method to register new services in IoC
        /// </summary>
        /// <param name="serviceCollection"> ServiceCollection class to extend </param>
        /// <returns> CointanerBuilder with new registered services </returns>
        public static IContainer AddApplicationModules(this IServiceCollection serviceCollection)
        {
            var builder = new ContainerBuilder();

            builder.RegisterModule(new InjectionModule());
            builder.Populate(serviceCollection);

            return builder.Build();
        }
    }
}