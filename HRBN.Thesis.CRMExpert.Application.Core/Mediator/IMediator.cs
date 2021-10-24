using System.Threading.Tasks;
using HRBN.Thesis.CRMExpert.Application.Core.Command;
using HRBN.Thesis.CRMExpert.Application.Core.Event;
using HRBN.Thesis.CRMExpert.Application.Core.Query;

namespace HRBN.Thesis.CRMExpert.Application.Core.Mediator
{
    public interface IMediator
    {
        Task<Result> CommandAsync<TCommand>(TCommand command) where TCommand : ICommand;
        Task<TResponse> QueryAsync<TResponse>(IQuery<TResponse> query);
        Task<TResponse> QueryAsync<TQuery, TResponse>(TQuery query) where TQuery : IQuery<TResponse>;

        void Publish<TEvent>(TEvent @event)
            where TEvent : IEvent;
    }
}