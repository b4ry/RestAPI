using Autofac;
using DivingApplicationAPI.DatabaseContext;
using DivingApplicationAPI.Queries;
using System.Linq;
using System.Reflection;
using System;
using DivingApplicationAPI.Commands;

namespace DivingApplicationAPI.DependencyInjection
{
    public class InjectionModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            RegisterQueries(builder);
            RegisterDatabaseSet(builder);
            RegisterCommands(builder);
        }

        private void RegisterCommands(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(ThisAssembly)
            .Where(x => x.IsAssignableTo<IHandleCommand>())
            .AsImplementedInterfaces();

            builder.Register(RegisterHandlersFactoryDelegate());

            builder.RegisterType<CommandBus>()
                .AsImplementedInterfaces();
        }

        private Func<IComponentContext, Func<Type, IHandleCommand>> RegisterHandlersFactoryDelegate()
        {
            return c =>
            {
                var ctx = c.Resolve<IComponentContext>();

                return ResolveCommand(ctx);
            };
        }

        private Func<Type, IHandleCommand> ResolveCommand(IComponentContext ctx)
        {
            return t =>
            {
                var handlerType = typeof(IHandleCommand<>).MakeGenericType(t);

                return (IHandleCommand)ctx.Resolve(handlerType);
            };
        }

        private void RegisterQueries(ContainerBuilder builder)
        {
            var repositoryAssembly = typeof(AccountQuery).GetTypeInfo().Assembly;

            builder.RegisterAssemblyTypes(repositoryAssembly)
                .Where(t => t.Name.EndsWith("Query"))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
        }

        private void RegisterDatabaseSet(ContainerBuilder builder)
        {
            builder.RegisterType<DatabaseSet>()
                .As<IDatabaseSet>()
                .InstancePerLifetimeScope();
        }
    }
}
