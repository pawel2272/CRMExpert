using System;
using HRBN.Thesis.CRMExpert.Application.Core.Command;

namespace HRBN.Thesis.CRMExpert.Application.CRMExpertDefinitions.Commands.User;

public sealed class ChangePasswordCommand : BaseCommand
{
    public Guid Id { get; set; }
    public string OldPassword { get; set; }
    public string NewPassword { get; set; }
    public string RepeatPassword { get; set; }
}