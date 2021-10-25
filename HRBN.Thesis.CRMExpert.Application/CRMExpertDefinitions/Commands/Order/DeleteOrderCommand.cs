using System;
using HRBN.Thesis.CRMExpert.Application.Core.Command;

namespace HRBN.Thesis.CRMExpert.Application.CRMExpertDefinitions.Commands.Order
{
    public class DeleteOrderCommand : ICommand
    {
        public DeleteOrderCommand()
        {
            
        }

        public DeleteOrderCommand(Guid id, Guid contactId)
        {
            ContactId = contactId;
            Id = id;
        }

        public Guid ContactId { get; set; }
        public Guid Id { get; set; }
        public Guid CommandKey { get; }
    }
}
