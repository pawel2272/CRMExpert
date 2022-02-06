using HRBN.Thesis.CRMExpert.Application.CRMExpertDefinitions.Dto;

namespace HRBN.Thesis.CRMExpert.UI.Models;

public class ProductViewModel
{
    public ProductDto Product { get; }

    public ProductViewModel(ProductDto product)
    {
        Product = product;
    }
}