using Microsoft.EntityFrameworkCore;
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

            try
            {
                handler.Handle(command);

                _logger.LogInformation($"Processed command '{command}'.", command);
            }
            catch (Exception e)
            {
                throw new DbUpdateException(message: $"Could not process '{command}'. Error: '{e.InnerException.Message}'", innerException: e);
            }
        }

        public async Task SendAsync<TCommand>(TCommand command) where TCommand : ICommand
        {
            var handler = (IHandleCommand<TCommand>)_noEntityHandlersFactory(typeof(TCommand));

            try
            {
                await handler.HandleAsync(command);

                _logger.LogInformation($"Processed command '{command}'.", command);
            }
            catch (Exception e)
            {
                if (e.InnerException != null) {
                    throw new DbUpdateException(message: $"Could not process '{command}'. Error: '{e.InnerException.Message}'", innerException: e);
                }
                else
                {
                    throw new DbUpdateException(message: $"Could not process '{command}'. Error: '{e.Message}'", innerException: e);
                }
            }
        }

        public void Send<TCommand, TEntity>(TCommand command, Expression<Func<TEntity, bool>> retrievalFunc) 
            where TCommand : ICommand
            where TEntity : BaseEntity
        {
            var handler = (IHandleCommand<TCommand, TEntity>)_entityHandlersFactory(typeof(TCommand), typeof(TEntity));

            try
            {
                handler.Handle(command, retrievalFunc);

                _logger.LogInformation($"Processed command '{command}'.", command);
            }
            catch (Exception e)
            {
                throw new DbUpdateException(message: $"Could not process '{command}'. Error: '{e.InnerException.Message}'", innerException: e);
            }
        }

        public async Task SendAsync<TCommand, TEntity>(TCommand command, Expression<Func<TEntity, bool>> retrievalFunc)
            where TCommand : ICommand
            where TEntity : BaseEntity
        {
            var handler = (IHandleCommand<TCommand, TEntity>)_entityHandlersFactory(typeof(TCommand), typeof(TEntity));

            try
            {
                await handler.HandleAsync(command, retrievalFunc);

                _logger.LogInformation($"Processed command '{command}'.", command);
            }
            catch (Exception e)
            {
                if (e.InnerException != null)
                {
                    throw new DbUpdateException(message: $"Could not process '{command}'. Error: '{e.InnerException.Message}'", innerException: e);
                }
                else
                {
                    throw new DbUpdateException(message: $"Could not process '{command}'. Error: '{e.Message}'", innerException: e);
                }
            }
        }
    }
}
