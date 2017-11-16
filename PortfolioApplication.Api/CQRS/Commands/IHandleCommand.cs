namespace PortfolioApplication.Api.CQRS.Commands
{
    public interface IHandleCommand
    {
    }

    public interface IHandleCommand<T> : IHandleCommand where T : ICommand
    {
        void Handle(T command);
    }
}
