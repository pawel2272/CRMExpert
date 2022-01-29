using System.Collections.Generic;
using HRBN.Thesis.CRMExpert.Application.CRMExpertDefinitions.Dto;
using HRBN.Thesis.CRMExpert.Domain.Core.Dto;

namespace HRBN.Thesis.CRMExpert.UI.ViewModels.User.Order;

public class OrderViewModel
{
    public OrderDto Order { get; }
    public List<ContactDataDto> ContactData { get; }
    public List<ProductDataDto> ProductData { get; }

    public OrderViewModel(OrderDto order, List<ContactDataDto> contactData, List<ProductDataDto> productData)
    {
        Order = order;
        ContactData = contactData;
        ProductData = productData;
    }
}