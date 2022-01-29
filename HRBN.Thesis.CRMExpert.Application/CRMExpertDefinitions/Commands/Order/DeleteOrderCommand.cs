using System;
using HRBN.Thesis.CRMExpert.Application.Core.Command;

namespace HRBN.Thesis.CRMExpert.Application.CRMExpertDefinitions.Commands.Order
{
    public class DeleteOrderCommand : BaseCommand
    {
        public DeleteOrderCommand()
        {
            
        }

        public DeleteOrderCommand(Guid id)
        {
            Id = id;
        }
        
        public Guid Id { get; set; }
    }
}
