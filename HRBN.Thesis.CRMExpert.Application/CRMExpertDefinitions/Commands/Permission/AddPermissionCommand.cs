using System;
using HRBN.Thesis.CRMExpert.Application.Core.Command;

namespace HRBN.Thesis.CRMExpert.Application.CRMExpertDefinitions.Commands.Permission
{
    public sealed class AddPermissionCommand : BaseCommand
    {
        public Guid UserId { get; set; }
        public Guid RoleId { get; set; }
    }
}