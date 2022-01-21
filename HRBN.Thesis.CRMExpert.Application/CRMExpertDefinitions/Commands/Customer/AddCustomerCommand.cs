using System;
using HRBN.Thesis.CRMExpert.Application.Core.Command;

namespace HRBN.Thesis.CRMExpert.Application.CRMExpertDefinitions.Commands.Customer
{
    public sealed class AddCustomerCommand : BaseCommand
    {
        public string Name { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string PostalCode { get; set; }
        public string TaxNo { get; set; }
        public string Regon { get; set; }
    }
}