using System;
using HRBN.Thesis.CRMExpert.Application.Core.Command;

namespace HRBN.Thesis.CRMExpert.Application.CRMExpertDefinitions.Commands.Contact
{
    public sealed class AddContactCommand : BaseCommand
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Street { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }
        public string ContactComment { get; set; }
        public Guid? UserId { get; set; }
        public Guid? CustomerId { get; set; }
    }
}