using System;

namespace HRBN.Thesis.CRMExpert.Application.CRMExpertDefinitions.Dto;

public class CustomerDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string City { get; set; }
    public string Street { get; set; }
    public string PostalCode { get; set; }
    public string TaxNo { get; set; }
    public string Regon { get; set; }
    public DateTime CreDate { get; set; }
    public DateTime ModDate { get; set; }
}