using Autofac;
using PortfolioApplication.Api.CQRS.Commands;
using PortfolioApplication.Services.DatabaseContexts;
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
            .Where(x => x.IsAssignableTo<ICommandHandler>())
            .AsImplementedInterfaces();

            builder.Register(RegisterNoEntityHandlersFactoryDelegate());
            builder.Register(RegisterEntityHandlersFactoryDelegate());

            builder.RegisterType<CommandBus>()
                .AsImplementedInterfaces();
        }

        private Func<IComponentContext, Func<Type, ICommandHandler>> RegisterNoEntityHandlersFactoryDelegate()
        {
            return c =>
            {
                var ctx = c.Resolve<IComponentContext>();

                return ResolveNoEntityCommand(ctx);
            };
        }

        private Func<Type, ICommandHandler> ResolveNoEntityCommand(IComponentContext ctx)
        {
            return command =>
            {
                var handlerType = typeof(ICommandHandler<>).MakeGenericType(command);

                return (ICommandHandler)ctx.Resolve(handlerType);
            };
        }

        private Func<IComponentContext, Func<Type, Type, ICommandHandler>> RegisterEntityHandlersFactoryDelegate()
        {
            return c =>
            {
                var ctx = c.Resolve<IComponentContext>();

                return ResolveEntityCommand(ctx);
            };
        }

        private Func<Type, Type, ICommandHandler> ResolveEntityCommand(IComponentContext ctx)
        {
            return (command, entity) =>
            {
                var handlerType = typeof(ICommandHandler<,>).MakeGenericType(command, entity);

                return (ICommandHandler)ctx.Resolve(handlerType);
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
