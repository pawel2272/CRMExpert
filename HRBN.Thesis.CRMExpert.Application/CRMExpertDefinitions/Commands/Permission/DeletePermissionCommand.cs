using System;
using HRBN.Thesis.CRMExpert.Application.Core.Command;

namespace HRBN.Thesis.CRMExpert.Application.CRMExpertDefinitions.Commands.Permission
{
    public class DeletePermissionCommand : BaseCommand
    {
        public DeletePermissionCommand(Guid id)
        {
            Id = id;
        }
        
        public Guid Id { get; set; }
    }
}
