using AutoMapper;
using System;
using System.Linq;
using System.Reflection;

namespace PortfolioApplication.Api.Mappings
{
    /// <summary>
    /// Static class used to configure AutoMapper
    /// </summary>
    public static class AutoMapperConfiguration
    {
        /// <summary>
        /// Configure method to register dynamically all of the application's profiles
        /// </summary>
        public static void Configure()
        {
            var profiles = typeof(Startup).GetTypeInfo().Assembly.GetTypes().Where(x => typeof(Profile).IsAssignableFrom(x));

            Mapper.Initialize(
                cfg =>
                {
                    foreach (var profile in profiles)
                    {
                        cfg.AddProfile(Activator.CreateInstance(profile) as Profile);
                    }
                }
            );
        }
    }
}
