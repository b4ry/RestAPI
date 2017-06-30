using System;
using Autofac;
using Microsoft.Extensions.DependencyInjection;
using Autofac.Extensions.DependencyInjection;
using DivingApplicationAPI.Queries;
using DivingApplicationAPI.DatabaseContext;

namespace DivingApplicationAPI.DependencyInjection
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