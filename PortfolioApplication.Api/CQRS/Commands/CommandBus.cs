using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace PortfolioApplication.Api.CQRS.Commands
{
    public class CommandBus : ICommandBus
    {
        private readonly Func<Type, IHandleCommand> _handlersFactory;
        private readonly ILogger<CommandBus> _logger;

        public CommandBus(Func<Type, IHandleCommand> handlersFactory, ILogger<CommandBus> logger)
        {
            _handlersFactory = handlersFactory;
            _logger = logger;
        }

        public void Send<TCommand>(TCommand command) where TCommand : ICommand
        {
            var handler = (IHandleCommand<TCommand>)_handlersFactory(typeof(TCommand));

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
            var handler = (IHandleCommand<TCommand>)_handlersFactory(typeof(TCommand));

            try
            {
                await handler.HandleAsync(command);

                _logger.LogInformation($"Processed command '{command}'.", command);
            }
            catch (Exception e)
            {
                throw new DbUpdateException(message: $"Could not process '{command}'. Error: '{e.InnerException.Message}'", innerException: e);
            }
        }
    }
}
