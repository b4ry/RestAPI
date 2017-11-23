using PortfolioApplication.Entities.Entities;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace PortfolioApplication.Api.CQRS.Commands
{
    public interface ICommandBus
    {
        void Send<TCommand>(TCommand command) 
            where TCommand : ICommand;
        void Send<TCommand, TEntity>(TCommand command, Expression<Func<TEntity, bool>> retrievalFunc) 
            where TCommand : ICommand 
            where TEntity : BaseEntity;
        Task SendAsync<TCommand>(TCommand command)
            where TCommand : ICommand;
        Task SendAsync<TCommand, TEntity>(TCommand command, Expression<Func<TEntity, bool>> retrievalFunc) 
            where TCommand : ICommand 
            where TEntity : BaseEntity;
    }
}
