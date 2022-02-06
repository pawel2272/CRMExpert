using System.Collections.Generic;
using HRBN.Thesis.CRMExpert.Application.CRMExpertDefinitions.Dto;
using HRBN.Thesis.CRMExpert.Domain.Core.Dto;

namespace HRBN.Thesis.CRMExpert.UI.Models;

public class DiscountViewModel
{
    public DiscountDto Discount { get; }
    public List<ProductDataDto> ProductData { get; }
    public List<CustomerDataDto> CustomerData { get; }

    public DiscountViewModel(DiscountDto discount, List<ProductDataDto> productData, List<CustomerDataDto> customerData)
    {
        Discount = discount;
        ProductData = productData;
        CustomerData = customerData;
    }
}