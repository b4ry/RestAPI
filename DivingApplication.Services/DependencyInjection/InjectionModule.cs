using Autofac;
using DivingApplication.Services.CQRS.Commands;
using DivingApplication.Services.CQRS.Queries;
using DivingApplication.Services.DatabaseContext;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace DivingApplication.Services.DependencyInjection
{
    public class InjectionModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            RegisterQueries(builder);
            RegisterDatabaseSet(builder);
            RegisterCommands(builder);
            RegisterCommandHandlers(builder);
            RegisterUnitOfWork(builder);
        }

        private void RegisterCommands(ContainerBuilder builder)
        {
            var commands = typeof(ICommand).GetTypeInfo().Assembly.GetTypes().Where(retCom => retCom.Name.EndsWith("Command") && !retCom.Name.StartsWith("I"));

            foreach(var command in commands)
            {
                var interfaceType = command.GetInterfaces().FirstOrDefault();

                builder.RegisterType(command).As(interfaceType).InstancePerLifetimeScope();
            }
        }

        private void RegisterCommandHandlers(ContainerBuilder builder)
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

                var commands = typeof(ICommand).GetTypeInfo().Assembly.GetTypes().Where(retCom => retCom.Name.EndsWith("Command") && !retCom.Name.StartsWith("I"));

                foreach (var command in commands)
                {
                    if(command.GetInterfaces().Contains(t))
                    {
                        handlerType = typeof(IHandleCommand<>).MakeGenericType(command);
                        break;
                    }  
                }

                return (IHandleCommand)ctx.Resolve(handlerType);
            };
        }

        private void RegisterQueries(ContainerBuilder builder)
        {
            var repositoryAssembly = typeof(DivingGearQuery).GetTypeInfo().Assembly;

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

        private void RegisterUnitOfWork(ContainerBuilder builder)
        {
            builder.RegisterType<UnitOfWork>()
                .As<IUnitOfWork>()
                .InstancePerLifetimeScope();
        }
    }
}
