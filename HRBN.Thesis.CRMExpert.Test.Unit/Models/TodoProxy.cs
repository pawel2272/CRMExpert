using System;
using HRBN.Thesis.CRMExpert.Domain.Core.Entities;

namespace HRBN.Thesis.CRMExpert.Test.Unit.Models
{
    public class TodoProxy : Todo
    {
        public TodoProxy(string title, string content)
        {
            this.Id = Guid.NewGuid();
            this.Title = title;
            this.Content = content;
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
