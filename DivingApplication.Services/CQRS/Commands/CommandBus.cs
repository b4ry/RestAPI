using System;
using System.Reflection;
using System.Threading.Tasks;

namespace DivingApplication.Services.CQRS.Commands
{
    public class CommandBus : ICommandBus
    {
        private readonly Func<Type, IHandleCommand> _handlersFactory;

        public CommandBus(Func<Type, IHandleCommand> handlersFactory)
        {
            _handlersFactory = handlersFactory;
        }

        public async void Send<TCommand>(TCommand command) where TCommand : ICommand
        {
            var handler = _handlersFactory(typeof(TCommand));

            MethodInfo method = handler.GetType().GetMethod("Handle");
            method.Invoke(handler, new object[] { command });
        }
    }
}
