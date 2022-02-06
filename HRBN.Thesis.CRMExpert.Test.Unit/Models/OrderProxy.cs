using System;
using HRBN.Thesis.CRMExpert.Domain.Core.Entities;

namespace HRBN.Thesis.CRMExpert.Test.Unit.Models
{
    public class OrderProxy : Order
    {
        public OrderProxy(string title, string content, decimal price, decimal count)
        {
            this.Id = Guid.NewGuid();
            this.Title = title;
            this.Content = content;
            this.Price = price;
            this.Count = count;
            this.CreDate = DateTime.Now;
            this.ModDate = DateTime.Now;
        }

        public void AddContact(Contact contact)
        {
            this.ContactId = contact.Id;
            this.Contact = contact;
        }
    }
}
