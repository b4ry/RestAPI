using AutoMapper;
using PortfolioApplication.Api.Mappings;
using System.Linq;
using System.Reflection;
using Xunit;

namespace PortfolioApplication.Tests.Mappings
{
    public class TestAutoMapperProfiles
    {
        [Fact(Skip = "AutoMapper is no longer configured this way - it is registered in IoC now (might change in the future)")]
        public void ApplicationMappingProfilesMustBeRegisteredWhenAutoMapperIsConfigured()
        {
            AutoMapperConfiguration.Configure();

            var profiles = Assembly.Load("PortfolioApplication.Api").GetTypes().Where(x => typeof(Profile).IsAssignableFrom(x));
            MethodInfo getProfilesMethodInfo = Mapper.Configuration.GetType().GetMethod("get_Profiles");
            ProfileMap[] registeredProfiles = (ProfileMap[])getProfilesMethodInfo.Invoke(Mapper.Configuration, new object[] { });

            bool allProfilesRegistered = true;

            foreach(var profile in profiles)
            {
                bool profileRegistered = registeredProfiles.Any(x => x.Name == profile.FullName);

                if(!profileRegistered)
                {
                    allProfilesRegistered = false;
                    break;
                }
            }

            Assert.True(allProfilesRegistered, "Did not register all of the application's mapping profiles");
        }
    }
}
