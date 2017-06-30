using AutoMapper;
using System;
using System.Linq;
using System.Reflection;

namespace CrankBankAPI.Mappings
{
    public class AutoMapperConfiguration
    {
        public static void Configure()
        {
            var profiles = typeof(Startup).GetTypeInfo().Assembly.GetTypes()
                .Where(x => typeof(Profile).IsAssignableFrom(x));

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
