using AutoMapper;
using PortfolioApplication.Api.DataTransferObjects;
using PortfolioApplication.Api.Mappings;
using PortfolioApplication.Entities.Entities;
using System.Linq;
using System.Reflection;
using Xunit;

namespace PortfolioApplication.Tests.Mappings
{
    public class TestAutoMapperProfiles
    {
        [Fact]
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

        [Fact]
        public void TechnologyTypeEntityToTechnologyTypeDtoReverseMappingConfigurationMustBeValid()
        {
            Mapper.Initialize(cfg => cfg.CreateMap<TechnologyTypeEntity, TechnologyTypeDto>().ReverseMap());

            Mapper.Configuration.AssertConfigurationIsValid();
        }

        [Fact]
        public void ProjectTypeEntityToProjectTypeDtoReverseMappingConfigurationMustBeValid()
        {
            Mapper.Initialize(cfg => cfg.CreateMap<ProjectTypeEntity, ProjectTypeDto>().ReverseMap());

            Mapper.Configuration.AssertConfigurationIsValid();
        }
    }
}
