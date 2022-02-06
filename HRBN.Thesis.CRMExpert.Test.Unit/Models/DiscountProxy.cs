using HRBN.Thesis.CRMExpert.Domain.Core.Entities;

namespace HRBN.Thesis.CRMExpert.Test.Unit.Models;

public class DiscountProxy : Discount
{
    public DiscountProxy(decimal value) : base()
    {
        this.DiscountVaule = value;
    }

    public void AddProduct(Product product)
    {
        this.Product = product;
        this.ProductId = product.Id;
    }

    public void AddCustomer(Customer customer)
    {
        this.Customer = customer;
        this.CustomerId = customer.Id;
    }
}