namespace PortfolioApplication.Services.CQRS.Queries
{
    public interface IQueryBus
    {
        void Send<TQuery>(TQuery query) where TQuery : IQuery;
    }
}
