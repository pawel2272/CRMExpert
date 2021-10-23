using HRBN.Thesis.CRMExpert.Application.Core.Command;

namespace HRBN.Thesis.CRMExpert.Application.Core.Mediator
{
    public interface IMediator
    {
        Result Command<TCommand>(TCommand command)
            where TCommand : ICommand;

        Result<TResult> Query<TResult>(IQuery<TResult> query);

        Result<TResult> Query<TQuery, TResult>(TQuery query)
            where TQuery : IQuery<TResult>;

        void Publish<TEvent>(TEvent @event)
            where TEvent : IEvent;
    }
}