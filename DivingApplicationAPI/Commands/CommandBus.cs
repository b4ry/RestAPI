using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DivingApplicationAPI.Commands
{
    public class CommandBus : ICommandBus
    {
        private readonly Func<Type, IHandleCommand> _handlersFactory;

        public CommandBus(Func<Type, IHandleCommand> handlersFactory)
        {
            _handlersFactory = handlersFactory;
        }

        public void Send<TCommand>(TCommand command) where TCommand : ICommand
        {
            var handler = (IHandleCommand<TCommand>)_handlersFactory(typeof(TCommand));

            handler.Handle(command);
        }
    }
}
