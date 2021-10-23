using System;
using HRBN.Thesis.CRMExpert.Application.Core.Command;

namespace HRBN.Thesis.CRMExpert.Application.Core.Mediator
{
    public class Mediator : IMediator
    {
        private readonly IDependencyResolver _dependencyResolver;
        
        public Mediator(IDependencyResolver dependencyResolver)
        {
            _dependencyResolver = dependencyResolver;
        }
        
        public Result Command<TCommand>(TCommand command) where TCommand : ICommand
        {
            var handler = _dependencyResolver.ResolveOrDefault<ICommandHandler<TCommand>>();
            if (handler == null)
            {
                throw new InvalidOperationException(
                    $"Command of type '{command.GetType()}' has not registered handler.");
            }

            return handler.Handle(command);
        }

        public Result<TResult> Query<TResult>(IQuery<TResult> query)
        {
            throw new System.NotImplementedException();
        }

        public Result<TResult> Query<TQuery, TResult>(TQuery query) where TQuery : IQuery<TResult>
        {
            throw new System.NotImplementedException();
        }

        public void Publish<TEvent>(TEvent @event) where TEvent : IEvent
        {
            throw new System.NotImplementedException();
        }
    }
}