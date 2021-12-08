using System;
using HRBN.Thesis.CRMExpert.Application.Core.Command;

namespace HRBN.Thesis.CRMExpert.Application.CRMExpertDefinitions.Commands.Todo
{
    public sealed class DeleteTodoCommand : ICommand
    {
        public DeleteTodoCommand()
        {
            
        }

        public DeleteTodoCommand(Guid id, Guid contactId)
        {
            ContactId = contactId;
            Id = id;
        }

        public Guid ContactId { get; set; }
        public Guid Id { get; set; }
        public Guid CommandKey { get; }
    }
}
