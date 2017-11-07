namespace PortfolioApplication.Services.CQRS.Queries
{
    public interface IHandleQuery
    {
    }

    public interface IHandleQuery<T> : IHandleQuery where T : IQuery
    {
        void Handle(T query);
    }
}
