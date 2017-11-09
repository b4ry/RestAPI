using Autofac;
using Autofac.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PortfolioApplication.Api.Extensions;
using PortfolioApplication.Services.CQRS.Queries;
using PortfolioApplication.Services.DatabaseContext;
using System;
using System.Linq;
using System.Reflection;
using Xunit;

namespace PortfolioApplication.Tests.Services.DependencyInjection
{
    public class TestDependencyInjection
    {
        [Fact]
        public void UnitOfWorkMustBeRegisteredWhenInversionOfControlContainerIsRegistered()
        {
            IServiceCollection serviceCollection = new ServiceCollection();
            var container = serviceCollection.AddApplicationModules();

            container.ComponentRegistry.TryGetRegistration(new TypedService(typeof(IUnitOfWork)), out IComponentRegistration componentRegistration);

            Assert.Equal(componentRegistration.Activator.LimitType.FullName, typeof(UnitOfWork).FullName);
        }

        [Fact]
        public void InversionOfControlContainerMustResolveUnitOfWorkWhenUnitOfWorkIsNeeded()
        {
            IServiceCollection serviceCollection = new ServiceCollection();
            serviceCollection.AddDbContext<PortfolioApplicationDbContext>(o => o.UseInMemoryDatabase(Guid.NewGuid().ToString()));
            IContainer container = serviceCollection.AddApplicationModules();

            IUnitOfWork resolvedComponent = container.Resolve<IUnitOfWork>();

            Assert.IsType(typeof(UnitOfWork), resolvedComponent);
        }

        [Fact]
        public void DatabaseSetMustBeRegisteredWhenInversionOfControlContainerIsRegistered()
        {
            IServiceCollection serviceCollection = new ServiceCollection();
            var container = serviceCollection.AddApplicationModules();

            container.ComponentRegistry.TryGetRegistration(new TypedService(typeof(IDatabaseSet)), out IComponentRegistration componentRegistration);

            Assert.Equal(componentRegistration.Activator.LimitType.FullName, typeof(DatabaseSet).FullName);
        }

        [Fact]
        public void InversionOfControlContainerMustResolveDatabaseSetWhenDatabaseSetIsNeeded()
        {
            IServiceCollection serviceCollection = new ServiceCollection();
            serviceCollection.AddDbContext<PortfolioApplicationDbContext>(o => o.UseInMemoryDatabase(Guid.NewGuid().ToString()));
            IContainer container = serviceCollection.AddApplicationModules();

            IDatabaseSet resolvedComponent = container.Resolve<IDatabaseSet>();

            Assert.IsType(typeof(DatabaseSet), resolvedComponent);
        }

        [Fact]
        public void QueriesMustBeRegisteredWhenInversionOfControlContainerIsRegistered()
        {
            IServiceCollection serviceCollection = new ServiceCollection();
            IComponentRegistration componentRegistration;
            var container = serviceCollection.AddApplicationModules();
            var allQueriesRegistered = true;

            var queries = Assembly
                .Load("PortfolioApplication.Services")
                .GetTypes()
                .Where(
                    x => x.GetTypeInfo().Name.EndsWith("Query") &&
                    x.IsInterface
                );

            foreach (Type query in queries)
            {
                componentRegistration = null;

                container.ComponentRegistry.TryGetRegistration(new TypedService(query), out componentRegistration);

                if (componentRegistration == null)
                {
                    allQueriesRegistered = false;
                    break;
                }
            }

            Assert.True(allQueriesRegistered, "Did not register all of the queries");
        }

        [Fact]
        public void InversionOfControlContainerMustResolveTechnologyTypeEntityQueryWhenItIsNeeded()
        {
            IServiceCollection serviceCollection = new ServiceCollection();
            serviceCollection.AddDbContext<PortfolioApplicationDbContext>(o => o.UseInMemoryDatabase(Guid.NewGuid().ToString()));
            serviceCollection.AddDistributedRedisCache(o => 
            {
                o.Configuration = "testConfiguration";
                o.InstanceName = "testInstanceName";
            });
            var container = serviceCollection.AddApplicationModules();
            
            var resolvedComponent = container.Resolve<ITechnologyTypeQuery>();

            Assert.IsType(typeof(TechnologyTypeQuery), resolvedComponent);
        }

        [Fact]
        public void InversionOfControlContainerMustResolveProjectTypeEntityQueryWhenItIsNeeded()
        {
            IServiceCollection serviceCollection = new ServiceCollection();
            serviceCollection.AddDbContext<PortfolioApplicationDbContext>(o => o.UseInMemoryDatabase(Guid.NewGuid().ToString()));
            serviceCollection.AddDistributedRedisCache(o =>
            {
                o.Configuration = "testConfiguration";
                o.InstanceName = "testInstanceName";
            });
            var container = serviceCollection.AddApplicationModules();

            var resolvedComponent = container.Resolve<IProjectTypeQuery>();

            Assert.IsType(typeof(ProjectTypeQuery), resolvedComponent);
        }
    }
}
