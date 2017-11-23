using Autofac;
using PortfolioApplication.Api.CQRS.Commands;
using PortfolioApplication.Entities.Entities;
using PortfolioApplication.Services.DatabaseContext;
using System;
using System.Linq;
using System.Reflection;

namespace PortfolioApplication.Api.DependencyInjection
{
    public class InjectionModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            RegisterQueries(builder);
            RegisterDatabaseSet(builder);
            RegisterCommands(builder);
            RegisterUnitOfWork(builder);
        }

        private void RegisterCommands(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(ThisAssembly)
            .Where(x => x.IsAssignableTo<IHandleCommand>())
            .AsImplementedInterfaces();

            builder.Register(RegisterNoEntityHandlersFactoryDelegate());
            builder.Register(RegisterEntityHandlersFactoryDelegate());

            builder.RegisterType<CommandBus>()
                .AsImplementedInterfaces();
        }

        private Func<IComponentContext, Func<Type, IHandleCommand>> RegisterNoEntityHandlersFactoryDelegate()
        {
            return c =>
            {
                var ctx = c.Resolve<IComponentContext>();

                return ResolveNoEntityCommand(ctx);
            };
        }

        private Func<Type, IHandleCommand> ResolveNoEntityCommand(IComponentContext ctx)
        {
            return command =>
            {
                var handlerType = typeof(IHandleCommand<>).MakeGenericType(command);

                return (IHandleCommand)ctx.Resolve(handlerType);
            };
        }

        private Func<IComponentContext, Func<Type, Type, IHandleCommand>> RegisterEntityHandlersFactoryDelegate()
        {
            return c =>
            {
                var ctx = c.Resolve<IComponentContext>();

                return ResolveEntityCommand(ctx);
            };
        }

        private Func<Type, Type, IHandleCommand> ResolveEntityCommand(IComponentContext ctx)
        {
            return (command, entity) =>
            {
                var handlerType = typeof(IHandleCommand<,>).MakeGenericType(command, entity);

                return (IHandleCommand)ctx.Resolve(handlerType);
            };
        }

        private void RegisterQueries(ContainerBuilder builder)
        {
            var repositoryAssembly = Assembly.Load("PortfolioApplication.Api");

            builder.RegisterAssemblyTypes(repositoryAssembly)
                .Where(t => (t.Name.EndsWith("Query") && t.Name != "Query"))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
        }

        private void RegisterDatabaseSet(ContainerBuilder builder)
        {
            builder.RegisterType<DatabaseSet>()
                .As<IDatabaseSet>()
                .InstancePerLifetimeScope();
        }

        private void RegisterUnitOfWork(ContainerBuilder builder)
        {
            builder.RegisterType<UnitOfWork>()
                .As<IUnitOfWork>()
                .InstancePerLifetimeScope();
        }
    }
}
