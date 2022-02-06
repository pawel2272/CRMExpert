using System;
using HRBN.Thesis.CRMExpert.Domain.Core.Entities;

namespace HRBN.Thesis.CRMExpert.Test.Unit.Models
{
    public class UserProxy : User
    {
        public UserProxy(string username,
            string gender,
            string password,
            string firstName,
            string lastName,
            string phone,
            string email,
            string street,
            string postalCode,
            string city) : base()
        {
            this.Id = Guid.NewGuid();
            this.Username = username;
            this.Gender = gender;
            this.Password = password;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Phone = phone;
            this.Email = email;
            this.Street = street;
            this.PostalCode = postalCode;
            this.City = city;
            this.ModDate = DateTime.Now;
            this.CreDate = DateTime.Now;
        }

        public void AddPermission(Permission permission)
        {
            this.Permissions.Add(permission);
        }

        public void AddContact(Contact contact)
        {
            this.Contacts.Add(contact);
        }
    }
}
