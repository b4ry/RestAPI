using Microsoft.Extensions.Logging;
using PortfolioApplication.Entities.Entities;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace PortfolioApplication.Api.CQRS.Commands
{
    public class CommandBus : ICommandBus
    {
        private readonly Func<Type, IHandleCommand> _noEntityHandlersFactory;
        private readonly Func<Type, Type, IHandleCommand> _entityHandlersFactory;
        private readonly ILogger<CommandBus> _logger;

        public CommandBus(
            Func<Type, IHandleCommand> noEntityHandlersFactory,
            Func<Type, Type, IHandleCommand> entityHandlersFactory,
            ILogger<CommandBus> logger)
        {
            _noEntityHandlersFactory = noEntityHandlersFactory;
            _entityHandlersFactory = entityHandlersFactory;
            _logger = logger;
        }

        public void Send<TCommand>(TCommand command) where TCommand : ICommand
        {
            var handler = (IHandleCommand<TCommand>)_noEntityHandlersFactory(typeof(TCommand));

            handler.Handle(command);
            _logger.LogInformation($"Processed command '{command}'.", command);
        }

        public async Task SendAsync<TCommand>(TCommand command) where TCommand : ICommand
        {
            var handler = (IHandleCommand<TCommand>)_noEntityHandlersFactory(typeof(TCommand));

            await handler.HandleAsync(command);
            _logger.LogInformation($"Processed command '{command}'.", command);
        }

        public void Send<TCommand, TEntity>(TCommand command, Expression<Func<TEntity, bool>> retrievalFunc)
            where TCommand : ICommand
            where TEntity : BaseEntity
        {
            var handler = (IHandleCommand<TCommand, TEntity>)_entityHandlersFactory(typeof(TCommand), typeof(TEntity));

            handler.Handle(command, retrievalFunc);
            _logger.LogInformation($"Processed command '{command}'.", command);
        }

        public async Task SendAsync<TCommand, TEntity>(TCommand command, Expression<Func<TEntity, bool>> retrievalFunc)
            where TCommand : ICommand
            where TEntity : BaseEntity
        {
            var handler = (IHandleCommand<TCommand, TEntity>)_entityHandlersFactory(typeof(TCommand), typeof(TEntity));

            await handler.HandleAsync(command, retrievalFunc);
            _logger.LogInformation($"Processed command '{command}'.", command);
        }
    }
}
