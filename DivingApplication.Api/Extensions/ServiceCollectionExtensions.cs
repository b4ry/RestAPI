using Autofac;
using Autofac.Extensions.DependencyInjection;
using DivingApplication.Services.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

namespace DivingApplication.Api.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IContainer AddApplicationModules(this IServiceCollection serviceCollection)
        {
            var builder = new ContainerBuilder();

            builder.RegisterModule(new InjectionModule());
            builder.Populate(serviceCollection);

            return builder.Build();
        }
    }
}