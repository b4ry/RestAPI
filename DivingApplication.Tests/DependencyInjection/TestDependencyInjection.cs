using Autofac;
using Autofac.Core;
using PortfolioApplication.Api.Extensions;
using PortfolioApplication.Services.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using Xunit;

namespace PortfolioApplication.Tests.DependencyInjection
{
    public class TestDependencyInjection
    {
        [Fact]
        public void UnitOfWorkMustBeRegisteredWhenInversionOfControlContainerIsRegistered()
        {
            IServiceCollection serviceCollection = new ServiceCollection();
            IComponentRegistration componentRegistration = null;
            var container = serviceCollection.AddApplicationModules();

            container.ComponentRegistry.TryGetRegistration(new TypedService(typeof(IUnitOfWork)), out componentRegistration);
            var resolvedComponentType = componentRegistration.Activator.LimitType.GetType();

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
        public void DatabaseSetsMustBeRegisteredWhenInversionOfControlContainerIsRegistered()
        {
            IServiceCollection serviceCollection = new ServiceCollection();
            IComponentRegistration componentRegistration = null;
            var container = serviceCollection.AddApplicationModules();

            container.ComponentRegistry.TryGetRegistration(new TypedService(typeof(IDatabaseSet)), out componentRegistration);
            var resolvedComponentType = componentRegistration.Activator.LimitType.GetType();

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
    }
}
