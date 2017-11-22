using System.Threading.Tasks;

namespace PortfolioApplication.Api.CQRS.Commands
{
    public interface IHandleCommand
    {
    }

    public interface IHandleCommand<TCommand> : IHandleCommand where TCommand : ICommand
    {
        void Handle(TCommand command);
        Task HandleAsync(TCommand command);
    }
}
