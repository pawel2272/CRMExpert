using System;
using HRBN.Thesis.CRMExpert.Application.Core.Command;

namespace HRBN.Thesis.CRMExpert.Application.CRMExpertDefinitions.Commands.Discount
{
    public class DeleteDiscountCommand : BaseCommand
    {
        public DeleteDiscountCommand(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }
    }
}
