using PortfolioApplication.Entities.Entities;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace PortfolioApplication.Api.CQRS.Commands
{
    public interface IHandleCommand
    {
    }

    public interface IHandleCommand<TCommand> : IHandleCommand 
        where TCommand : ICommand
    {
        void Handle(TCommand command);
        Task HandleAsync(TCommand command);
    }

    public interface IHandleCommand<TCommand, TEntity> : IHandleCommand
        where TCommand : ICommand
        where TEntity : BaseEntity
    {
        void Handle(TCommand command, Expression<Func<TEntity, bool>> retrievalFunc);
        Task HandleAsync(TCommand command, Expression<Func<TEntity, bool>> retrievalFunc);
    }
}
