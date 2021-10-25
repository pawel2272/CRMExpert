using System;
using HRBN.Thesis.CRMExpert.Application.Core.Command;

namespace HRBN.Thesis.CRMExpert.Application.CRMExpertDefinitions.Commands.Role
{
    public sealed class EditRoleCommand : ICommand
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid CommandKey { get; }
    }
}
