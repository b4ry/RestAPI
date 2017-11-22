using System.Threading.Tasks;

namespace PortfolioApplication.Api.CQRS.Commands
{
    public interface ICommandBus
    {
        void Send<TCommand>(TCommand command) where TCommand : ICommand;
        Task SendAsync<TCommand>(TCommand command) where TCommand : ICommand;
    }
}
