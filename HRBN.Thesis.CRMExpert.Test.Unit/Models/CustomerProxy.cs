using System;
using System.Collections.Generic;
using HRBN.Thesis.CRMExpert.Domain.Core.Entities;

namespace HRBN.Thesis.CRMExpert.Test.Unit.Models;

public class CustomerProxy : Customer
{
    public CustomerProxy(string name,
        string street,
        string postalCode,
        string city,
        string taxNo,
        string regon) : base()
    {
        this.Id = Guid.NewGuid();
        this.Name = name;
        this.Street = street;
        this.PostalCode = postalCode;
        this.City = city;
        this.TaxNo = taxNo;
        this.Regon = regon;
        this.Contacts = new List<Contact>();
        this.Users = new List<User>();
        this.Discounts = new List<Discount>();
    }

    public void AddContact(Contact contact)
    {
        this.Contacts.Add(contact);
    }
    
    public void AddUser(User user)
    {
        this.Users.Add(user);
    }
    
    public void AddDiscount(Discount discount)
    {
        this.Discounts.Add(discount);
    }
}