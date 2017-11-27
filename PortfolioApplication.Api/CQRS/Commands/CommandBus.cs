using Microsoft.Extensions.Logging;
using PortfolioApplication.Entities.Entities;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace PortfolioApplication.Api.CQRS.Commands
{
    public class CommandBus : ICommandBus
    {
        private readonly Func<Type, ICommandHandler> _noEntityHandlersFactory;
        private readonly Func<Type, Type, ICommandHandler> _entityHandlersFactory;
        private readonly ILogger<CommandBus> _logger;

        public CommandBus(
            Func<Type, ICommandHandler> noEntityHandlersFactory,
            Func<Type, Type, ICommandHandler> entityHandlersFactory,
            ILogger<CommandBus> logger)
        {
            _noEntityHandlersFactory = noEntityHandlersFactory;
            _entityHandlersFactory = entityHandlersFactory;
            _logger = logger;
        }

        public void Send<TCommand>(TCommand command) where TCommand : ICommand
        {
            var handler = (ICommandHandler<TCommand>)_noEntityHandlersFactory(typeof(TCommand));

            handler.Handle(command);
            _logger.LogInformation($"Processed command '{command}'.", command);
        }

        public async Task SendAsync<TCommand>(TCommand command) where TCommand : ICommand
        {
            var handler = (ICommandHandler<TCommand>)_noEntityHandlersFactory(typeof(TCommand));

            await handler.HandleAsync(command);
            _logger.LogInformation($"Processed command '{command}'.", command);
        }

        public void Send<TCommand, TEntity>(TCommand command, Expression<Func<TEntity, bool>> retrievalFunc)
            where TCommand : ICommand
            where TEntity : BaseEntity
        {
            var handler = (ICommandHandler<TCommand, TEntity>)_entityHandlersFactory(typeof(TCommand), typeof(TEntity));

            handler.Handle(command, retrievalFunc);
            _logger.LogInformation($"Processed command '{command}'.", command);
        }

        public async Task SendAsync<TCommand, TEntity>(TCommand command, Expression<Func<TEntity, bool>> retrievalFunc)
            where TCommand : ICommand
            where TEntity : BaseEntity
        {
            var handler = (ICommandHandler<TCommand, TEntity>)_entityHandlersFactory(typeof(TCommand), typeof(TEntity));

            await handler.HandleAsync(command, retrievalFunc);
            _logger.LogInformation($"Processed command '{command}'.", command);
        }
    }
}
