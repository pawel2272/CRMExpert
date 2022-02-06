using System;

namespace HRBN.Thesis.CRMExpert.Application.CRMExpertDefinitions.Dto;

public class DiscountDto
{
    public Guid Id { get; set; }
    public decimal DiscountVaule { get; set; }
    public string CustomerName { get; set; }
    public string ProductName { get; set; }
    public Guid ProductId { get; set; }
    public Guid CustomerId { get; set; }
    public DateTime CreDate { get; set; }
    public DateTime ModDate { get; set; }
}