using System;

namespace HRBN.Thesis.CRMExpert.Application.CRMExpertDefinitions.Dto;

public class ProductDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public string Description { get; set; }
    public string Type { get; set; }
    public decimal Count { get; set; }
    public DateTime CreDate { get; set; }
    public DateTime ModDate { get; set; }
}