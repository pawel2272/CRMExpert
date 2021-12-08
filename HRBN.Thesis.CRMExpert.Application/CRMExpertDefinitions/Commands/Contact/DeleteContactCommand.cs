using System;
using HRBN.Thesis.CRMExpert.Application.Core.Command;

namespace HRBN.Thesis.CRMExpert.Application.CRMExpertDefinitions.Commands.Contact
{
    public class DeleteContactCommand : ICommand
    {
        public DeleteContactCommand(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }
        public Guid CommandKey { get; }
    }
}
