using System.Collections.Generic;
using HRBN.Thesis.CRMExpert.Domain.Core.Entities;

namespace HRBN.Thesis.CRMExpert.Test.Unit.Models;

public class ProductProxy : Product
{
    public ProductProxy(string name, decimal price, string description, string type, decimal count) : base()
    {
        this.Name = name;
        this.Price = price;
        this.Description = description;
        this.Type = type;
        this.Count = count;
        this.Discounts = new List<Discount>();
        this.Orders = new List<Order>();
    }

    public void AddDiscount(Discount discount)
    {
        this.Discounts.Add(discount);
    }

    public void AddOrder(Order order)
    {
        this.Orders.Add(order);
    }
}