using System;
using HRBN.Thesis.CRMExpert.Application.Core.Command;

namespace HRBN.Thesis.CRMExpert.Application.CRMExpertDefinitions.Commands.User;

public sealed class ChangeAddressCommand : BaseCommand
{
    public Guid Id { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public string Street { get; set; }
    public string PostalCode { get; set; }
    public string City { get; set; }
}