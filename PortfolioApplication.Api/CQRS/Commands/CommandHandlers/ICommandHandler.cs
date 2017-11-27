using PortfolioApplication.Entities.Entities;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace PortfolioApplication.Api.CQRS.Commands
{
    public interface ICommandHandler
    {
    }

    public interface ICommandHandler<TCommand> : ICommandHandler 
        where TCommand : ICommand
    {
        void Handle(TCommand command);
        Task HandleAsync(TCommand command);
    }

    public interface ICommandHandler<TCommand, TEntity> : ICommandHandler
        where TCommand : ICommand
        where TEntity : BaseEntity
    {
        void Handle(TCommand command, Expression<Func<TEntity, bool>> retrievalFunc);
        Task HandleAsync(TCommand command, Expression<Func<TEntity, bool>> retrievalFunc);
    }
}
